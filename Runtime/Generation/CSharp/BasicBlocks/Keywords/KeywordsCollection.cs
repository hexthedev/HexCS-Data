using System;
using System.Collections.Generic;
using System.Text;
using HexCS.Core;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// A collection of CSharp keywords that can be output as a string
    /// in the following pattern "keyword keywork keyword ..."
    /// </summary>
    public class KeywordsCollection
    {
        private List<EKeyword> _keywords = new List<EKeyword>();

        /// <summary>
        /// Number of keywords added
        /// </summary>
        public int Count => _keywords.Count;

        /// <summary>
        /// Keywords currently added to collection
        /// </summary>
        public IEnumerable<EKeyword> Keywords => _keywords;

        /// <summary>
        /// Construct a keyword collection with keywords
        /// </summary>
        /// <param name="keywords"></param>
        public KeywordsCollection(params EKeyword[] keywords)
        {
            AddKeywords(keywords);
        }

        /// <summary>
        /// Adds keywords to back of collection in order
        /// </summary>
        /// <param name="keywords">keywords to add</param>
        public void AddKeywords(params EKeyword[] keywords)
        {
            _keywords.AddRange(keywords);
        }

        /// <summary>
        /// Returns keyword colleciton as usable keyword string. Ends in " " character.
        /// </summary>
        /// <returns></returns>
        public string ToKeywordString()
        {
            if(_keywords.Count == 0)
            {
                return string.Empty;
            }
            else
            {
                StringBuilder sb = new StringBuilder();

                foreach (EKeyword word in _keywords)
                {
                    sb.Append($"{word.ToCSharpString()} ");
                }

                return sb.ToString();
            }
        }
    }
}
