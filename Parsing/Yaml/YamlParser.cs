using System;
using System.Collections.Generic;
using System.Text;
using HexCS.Core;
using HexCS.Data.Runtime;

namespace HexCS.Data.Parsing.Yaml
{
    /// <summary>
    /// <para>The Tobias YAML Parser is not designed to parse the entire yaml spec, rather it 
    /// is designed to parser a stricter subset. As time goes on I'll increase the features but for my
    /// current needs, I just want somethin that works and is human readable. </para>
    /// 
    /// <para>The parser works by stepping forward in the yaml parse string as a large array. I've
    /// minimize string copies as much as possible for efficientcy. As time goes on I will improve this parser, 
    /// So I've written a few tests with examples</para>
    /// 
    /// <para> A quick explainaiton of the spec: Files always use the YAML --- and *** end line characters. There are three
    /// types of elements: Objects, Dictionaries and Lists. Objects are key:value. Lists are value[]. Dictionary are 
    /// key:value[]</para>
    /// </summary>
    public class YamlParser : IDataParser
    {
        private readonly DCharQuery cNewlineQuery = UTCharQuery.GetQuery(ECharQuery.NewLine);

        #region API
        /// <summary>
        /// character used for start block in yaml
        /// </summary>
        public const string cYamlStartFileToken = "---";

        /// <summary>
        /// Char query used to query string index for yaml start of file token
        /// </summary>
        public static readonly DCharQuery cYamlStartFileTokenQuery = UTCharQuery.GetOccurenceBlockQuery(cYamlStartFileToken[0], cYamlStartFileToken.Length);

        /// <summary>
        /// character used for start block in yaml
        /// </summary>
        public const string cYamlEndFileToken = "***";

        /// <summary>
        /// Char query used to query string index for yaml start of file token
        /// </summary>
        public static readonly DCharQuery cYamlEndFileTokenQuery = UTCharQuery.GetOccurenceBlockQuery(cYamlEndFileToken[0], cYamlEndFileToken.Length);

        /// <summary>
        /// String representing yaml tab
        /// </summary>
        public const string cYamlTab = "  ";

        /// <summary>
        /// Char token used to detect yaml objects
        /// </summary>
        public const char cYamlObjectToken = ':';

        /// <summary>
        /// Char token used to detect yaml lists
        /// </summary>
        public const char cYamlListToken = '-';

        /// <summary>
        /// Char token used to detect yaml comment
        /// </summary>
        public const char cYamlCommentToken = '#';

        /// <inheritdoc />
        public InterObject Interpret(string yaml) {
            int index = 0;

            // Get to Yaml Start
            ScanToFirstYamlLine(ref yaml, ref index);

            // Get to First object
            ScanToNextObjectLine(ref yaml, ref index);
            return new InterObject(ParseDictionaryBlock(ref yaml, ref index));
        }
        #endregion

        /// <summary>
        /// Dictionaries are a same depth collection of objects. Blank lines are skipped. End line
        /// stops process. index should start at first line of dictionary. ends on line after completed
        /// DictionaryBlock.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="index"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        private InterField[] ParseDictionaryBlock(ref string s, ref int index, int depth = 0)
        {
            List<InterField> output = new List<InterField>();

            int lastIndex; // used to catch infinite loops
            do
            {
                lastIndex = index;

                // if end of file then stop
                if (DetectEndOfFile(ref s, index)) break; // keep on line, don't got to next

                // if it's a blank line just move on
                if (DetectIsBlankLine(ref s, index))
                {
                    UTString.ScanToNewLineStart(ref s, ref index);
                    continue;
                }

                // if the depth is wrong, then this block is complete
                if (DetectDepth(ref s, index) != depth) break; // keep on line, don't go to next

                // Parse the line as an object block. Since this is a dictionary the only
                // valid lines at this depth are object definitions

                output.Add(ParseObjectBlock(ref s, ref index, depth));  // should move to next line after block

            } while (index != lastIndex);

            return output.ToArray();
        }

        /// <summary>
        /// Parses an Object Block at a depth. Assumes that index is at the start of the object block line.
        /// Should end on line following block
        /// </summary>
        /// <param name="s"></param>
        /// <param name="index"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        private InterField ParseObjectBlock(ref string s, ref int index, int depth = 0)
        {
            // if depths don't match theres an issue
            if (DetectDepth(ref s, index) != depth) return InterField.Auto(null, false, "OBJECT BLOCK STARTS WITH INCORRECT DEPTH");

            // Extract the key
            int startIndex = index;
            if (!ExtractObjectKey(ref s, ref index, ref startIndex)) return InterField.Auto(null, false, "NO KEY FOUND");
            string key = s.Substring(startIndex, index - startIndex).TrimEnd();

            // Extract the value
            index++; // to step passed colont

            object block = ParseObjectBlockLine(ref s, ref index, out bool isArray, depth);
            return InterField.Auto(block, isArray, key);
        }

        /// <summary>
        /// Expects the index after the object colon. Handles the resolution of the object at block depth.
        /// After resolution automatically moves to next line. This is true if the object is a one liner
        /// or multi liner object
        /// </summary>
        /// <param name="s"></param>
        /// <param name="index"></param>
        /// <param name="depth"></param>
        /// <param name="isArray"></param>
        /// <returns></returns>
        private object ParseObjectBlockLine(ref string s, ref int index, out bool isArray, int depth = 0)
        {
            // try a single line value
            int startIndex = index;

            if (ExtractValue(ref s, ref index, ref startIndex))
            {
                string ext = s.Substring(startIndex, index - startIndex);
                UTString.ScanToNewLineStart(ref s, ref index);  // move to next line after extraction
                isArray = default;
                return ext;
            }
            // detect the multiline value
            else
            {
                UTString.ScanToNewLineStart(ref s, ref index);
                EObjectNestedType type = DetectObjectNestedType(ref s, index);

                if (type == EObjectNestedType.DICTIONARY)
                {
                    isArray = false;
                    return new InterObject(ParseDictionaryBlock(ref s, ref index, depth + 1)); // Should move to line after dictionary block automatically
                }
                else if (type == EObjectNestedType.LIST)
                {
                    isArray = true;
                    return ParseListBlock(ref s, ref index, depth + 1);// Should move to line after list block automatically
                }
            }

            isArray = default;
            return "INVALID";
        }

        /// <summary>
        /// Assumes start of line for a list block. parses the block and ends at the start of the preceeding line
        /// </summary>
        /// <param name="s"></param>
        /// <param name="index"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        private InterObject ParseListBlock(ref string s, ref int index, int depth = 0)
        {
            List<object> values = new List<object>();

            // read lines that have required depth
            do
            {
                // if end of file then stop
                if (DetectEndOfFile(ref s, index)) break; // keep on line, don't got to next

                // if it's a blank line just move on
                if (DetectIsBlankLine(ref s, index))
                {
                    UTString.ScanToNewLineStart(ref s, ref index);
                    continue;
                }

                // if the depth is wrong, then this block is complete
                if (DetectDepth(ref s, index) != depth) break; // keep on line, don't go to next

                if (DetectListUntilEndLine(ref s, index))
                {
                    values.Add(ParseListValue(ref s, ref index, depth)); // automoatically moves to next line
                }
            } while (true);

            //ScanToNewLineStart(ref s, ref index);
            return DataArray.AutoArray(values.ToArray()).ConvertToIntermediate();
        }

        /// <summary>
        /// Expects index after - character. Automatically moves to next line
        /// </summary>
        /// <param name="s"></param>
        /// <param name="index"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        private object ParseListValue(ref string s, ref int index, int depth = 0)
        {
            // is it an object
            bool isObject = DetectObjectUntilEndLine(ref s, index);

            // Parse as a dictionary
            if (isObject)
            {
                return ParseListObjectBlock(ref s, ref index, depth); // moves to line after block automatically
            }
            else
            {
                index++; // move passed the - value

                ScanYamlTabDepth(ref s, ref index);
                index++;

                int startIndex = index;
                if (ExtractValue(ref s, ref index, ref startIndex))
                {
                    string ext = s.Substring(startIndex, index - startIndex);
                    UTString.ScanToNewLineStart(ref s, ref index);
                    return ext;
                }

            }

            UTString.ScanToNewLineStart(ref s, ref index);
            return "NO LIST VALUE";
        }

        private object ParseListObjectBlock(ref string s, ref int index, int depth = 0)
        {
            // if depths don't match theres an issue
            if (DetectDepth(ref s, index) != depth) return null;

            // Extract the key
            int startIndex = index;
            if (!ExtractObjectKey(ref s, ref index, ref startIndex)) return null;
            string key = s.Substring(startIndex, index - startIndex).TrimEnd();

            // Extract the value
            index++; // to step passed colont

            return ParseObjectBlockLine(ref s, ref index, out bool isArray, depth);
        }

        #region Detectors
        /// <summary>
        /// Detect if this line is a blank line. BLank lines don't break blocks. 
        /// Lines that only contain comments are also considered blank lines
        /// </summary>
        /// <param name="s"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private bool DetectIsBlankLine(ref string s, int index)
        {
            bool isBlank = true;

            UTString.LoopCharacterUntilLine(ref s, index, (c) =>
            {
                if (c == cYamlCommentToken) return true; // if comment, break loop

                // if we hit a char that isn't whitespace then it's not blank
                if (!char.IsWhiteSpace(c))
                {
                    isBlank = false;
                    return true;
                }

                return false;
            });

            return isBlank;
        }

        /// <summary>
        /// Assumes index is at line after object declarion of a nested object. Without moving
        /// index, detects the object type (list or dictionary)
        /// </summary>
        /// <param name="s"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private EObjectNestedType DetectObjectNestedType(ref string s, int index)
        {
            EObjectNestedType response = EObjectNestedType.INVALID;

            UTString.LoopCharacterUntilStringEnd(ref s, index, (c) =>
            {
                if (c == cYamlObjectToken)
                {
                    response = EObjectNestedType.DICTIONARY;
                    return true;
                }

                if (c == cYamlListToken)
                {
                    response = EObjectNestedType.LIST;
                    return true;
                }

                return false;
            }
            );

            return response;
        }

        /// <summary>
        /// Without moving the index, detects the depth of the line by counting the
        /// tabs and detecting if a - exists (because list elements have + 1 for the - character)
        /// </summary>
        /// <param name="s"></param>
        /// <param name="index"></param>
        /// <param name="tabs"></param>
        /// <returns></returns>
        private int DetectDepth(ref string s, int index)
        {
            int dectectedTabs = ScanYamlTabDepth(ref s, ref index);
            if (s[index] == cYamlListToken) dectectedTabs++;
            return dectectedTabs;
        }

        /// <summary>
        /// Detects if an object is present in the line
        /// </summary>
        /// <param name="s"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private bool DetectObjectUntilEndLine(ref string s, int index)
        {
            return ScanForObjectColon(ref s, ref index);
        }

        /// <summary>
        /// Detects if an object is present in the line
        /// </summary>
        /// <param name="s"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private bool DetectListUntilEndLine(ref string s, int index)
        {
            return ScanForListCharacter(ref s, ref index);
        }

        /// <summary>
        /// Detects forward to the next newline character and if it finds the end of file ***
        /// before that, return true
        /// </summary>
        /// <param name="s"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private bool DetectEndOfFile(ref string s, int index)
        {
            // Find the start of the file
            for (; s[index] != UTCharQuery.cLinuxNewline; index++)
            {
                if (cYamlEndFileTokenQuery(ref s, index)) return true;
            }

            return false;
        }
        #endregion

        // Extractors move the provided index and start index to indicate a region tha meets some condition
        #region Extractors
        /// <summary>
        /// populates startIndex with the startinf index of the value, and index with the end index.
        /// Returns false if index not found. 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="index"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        private bool ExtractValue(ref string s, ref int index, ref int startIndex)
        {
            // Find the value start
            if (!ScanForValueStart(ref s, ref index)) return false;
            startIndex = index;

            // Scan to end index
            for (; index < s.Length; index++)
            {
                // if comment or newline, stop and test if you have a value
                if (cNewlineQuery(ref s, index) || s[index] == cYamlCommentToken)
                {
                    if (index - startIndex > 0)
                    {
                        return true; // you have a value
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Starting from index, updates the startindex and index variables to start and end of key.
        /// False if no key found. Assumes ke yis somewhere on same line as index at some greater index
        /// </summary>
        /// <param name="s"></param>
        /// <param name="index"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        private bool ExtractObjectKey(ref string s, ref int index, ref int startIndex)
        {
            if (!ScanForValueStart(ref s, ref index)) return false;
            startIndex = index;
            return ScanForObjectColon(ref s, ref index) && index > startIndex;
        }
        #endregion

        // Scans move the proivded index until some condition is met
        #region Scan
        /// <summary>
        /// Moves the index to the first non-whitespace character or returns false if newline is found before
        /// </summary>
        /// <param name="s"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private bool ScanForValueStart(ref string s, ref int index)
        {
            bool result = false;

            UTString.ScanUntil(ref s, ref index, (ref string st, int i) =>
            {
                // did we hit a non-whitespace character. Skip if - list character
                bool isValueStart = !char.IsWhiteSpace(st[i]) && st[i] != cYamlListToken;

                if (isValueStart || cNewlineQuery(ref st, i))
                {
                    result = isValueStart;
                    return true;
                }

                return false;
            });

            return result;
        }

        /// <summary>
        /// Moves the index to the next -, or returns false if no colon exists before a newline
        /// </summary>
        /// <param name="s"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private bool ScanForListCharacter(ref string s, ref int index)
        {
            bool result = false;

            UTString.ScanUntil(ref s, ref index, (ref string st, int i) =>
            {
                bool isYamlListToken = st[i] == cYamlListToken;

                if (isYamlListToken || st[i] == UTCharQuery.cLinuxNewline)
                {
                    result = isYamlListToken;
                    return true;
                }

                return false;
            });

            return result;
        }

        /// <summary>
        /// Moves index to next line containing an object
        /// </summary>
        /// <param name="s"></param>
        /// <param name="index"></param>
        private void ScanToNextObjectLine(ref string s, ref int index)
        {
            while (!DetectObjectUntilEndLine(ref s, index))
            {
                UTString.ScanToNewLineStart(ref s, ref index);
            }
        }

        /// <summary>
        /// Moves the index to the next :, or returns false if no colon exists before a newline
        /// </summary>
        /// <param name="s"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private bool ScanForObjectColon(ref string s, ref int index)
        {
            bool result = false;

            UTString.ScanUntil(ref s, ref index, (ref string st, int i) =>
            {
                bool isYamlObjectToken = st[i] == cYamlObjectToken;

                if (isYamlObjectToken || st[i] == UTCharQuery.cLinuxNewline)
                {
                    result = isYamlObjectToken;
                    return true;
                }

                return false;
            });

            return result;
        }

        /// <summary>
        /// Moves the index passed all occurences of ' '' ' and returns the number of tabs passed
        /// </summary>
        /// <param name="s"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private int ScanYamlTabDepth(ref string s, ref int index)
        {
            int depth = 0;

            for (; index + 1 < s.Length; index += 2)
            {
                // if found a tab at index
                if (s[index] == cYamlTab[0] && s[index + 1] == cYamlTab[1])
                {
                    // increase depth
                    depth++;
                }
                else // otherwise you have the depth
                {
                    break;
                }
            }

            return depth;
        }

        /// <summary>
        /// Moves the index to the NewlineStart after the YamlFileStart characters
        /// </summary>
        /// <param name="s"></param>
        /// <param name="index"></param>
        private void ScanToFirstYamlLine(ref string s, ref int index)
        {
            // Find BLock
            UTString.ScanUntil(ref s, ref index, cYamlStartFileTokenQuery);
            index += cYamlStartFileToken.Length;

            // Move to start of next line
            UTString.ScanToNewLineStart(ref s, ref index);
        }
        #endregion

        #region Internal Objects
        private enum EObjectNestedType
        {
            INVALID = 0,

            LIST = 1,

            DICTIONARY = 2
        }
        #endregion
    }
}
