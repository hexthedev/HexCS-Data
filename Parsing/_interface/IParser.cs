using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Parsing
{
    /// <summary>
    /// Parser for some string to output object
    /// </summary>
    /// <typeparam name="TOutput"></typeparam>
    public interface IParser<TOutput> : IInterpreter<string, TOutput> { }
}
