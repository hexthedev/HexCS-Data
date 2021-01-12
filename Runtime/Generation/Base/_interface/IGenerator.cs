using System;
using System.Text;

namespace HexCS.Data.Generation
{
    /// <summary>
    /// Generators are classes that have a generate function. The original purpose of Generate() is to
    /// write strings to an internally referenced StringBuilder(), which is passed through a tree
    /// to other Generators so that a complete file can be generated. 
    /// 
    /// Also, the developer facing behaviour of a generator is that Generate() is automatically called
    /// if Dispose() is called. This allows generators to be used in using() statements. This was done
    /// as a syntax experiment, to see if this made writing generation code easier. 
    /// </summary>
    public interface IGenerator : IDisposable
    {
        /// <summary>
        /// The level of indent that should be applied to the generated output
        /// </summary>
        IndentProvider IndentProvider { get; set; }

        /// <summary>
        /// Returns reference to the StringBuilder used at output
        /// </summary>
        StringBuilder OutputBuilder { get; set; }

        /// <summary>
        /// Pushes generated strings to an internal StringBuilder. 
        /// </summary>
        void Generate();
    }
}
