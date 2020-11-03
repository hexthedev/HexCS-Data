using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Parsing
{
    /// <summary>
    /// Interpretation units take some input element TInput and translate them to TOutput. 
    /// This is basically a single iteration of a transformation step over some data.
    /// </summary>
    /// <typeparam name="TInput">Input type</typeparam>
    /// <typeparam name="TOutput">Output type</typeparam>
    public interface IInterpreter<TInput, TOutput>
    {
        /// <summary>
        /// Interprets the input as an output
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        TOutput Interpret(TInput input);
    }
}
