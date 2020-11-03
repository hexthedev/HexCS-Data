using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using HexCS.Core;

namespace HexCS.Data.Parsing.Parsers
{
    /// <summary>
    /// Parses a YamlFile in 
    /// </summary>
    public class RowParserIDataYaml : IRowParser<KeyValuePair<string, object>[]>
    {
        private const string cYamlTab = "  ";
        private const char YamlListCharacter = '-';

        /// <inheritdoc/>
        public KeyValuePair<string, object>[] Interpret(string[] input)
        {
            List<KeyValuePair<string, object>> _pairs = new List<KeyValuePair<string, object>>();

            int processIndex = 0;
            while (processIndex < input.Length)
            {
                string target = input[processIndex];
                int colonIndex = target.IndexOf(':');

                // if there is a colon then a processing unity or a yaml object exists
                if (colonIndex > 0)
                {
                    // Find the section that is tabbed to get a full processing unit
                    processIndex++;
                    string nextLine = input[processIndex];

                    List<string> linesToProcess = new List<string>();

                    while (nextLine.StartsWith(cYamlTab))
                    {
                        linesToProcess.Add(nextLine);
                        processIndex++;

                        if (processIndex >= input.Length) break;
                    }

                    // Send lines for processing
                    // PROCESS YAML UNIT
                }
                else
                {
                    processIndex++;
                }
            }

            return default;
        }
    }
}
