using System;

namespace HexCS.Data.Parsing
{
    /// <summary>
    /// Parses each element of a CSV using a Parser
    /// </summary>
    /// <typeparam name="TOutput"></typeparam>
    public class CSVParser<TOutput> : IParser<TOutput[]>
    {
        /// <summary>
        /// Parser uses to parse each element of the csv
        /// </summary>
        public IParser<TOutput> ElementParser;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parser">Parser used to parse each elemetn in the CSV</param>
        public CSVParser(IParser<TOutput> parser)
        {
            ElementParser = parser ?? throw new ArgumentException("parser cannot equal null");
        }

        /// <inheritdoc />
        public TOutput[] Interpret(string input)
        {
            string[] element = input.Split(',');
            TOutput[] output = new TOutput[element.Length];

            for(int i = 0; i<element.Length; i++)
            {
                output[i] = ElementParser.Interpret(element[i]);
            }

            return output;
        }
    }
}
