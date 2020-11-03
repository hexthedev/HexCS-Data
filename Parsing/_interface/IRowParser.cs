using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Parsing
{
    /// <summary>
    /// Parses a row of strings
    /// </summary>
    public interface IRowParser<TOutput> : IInterpreter<string[], TOutput>{ }
}
