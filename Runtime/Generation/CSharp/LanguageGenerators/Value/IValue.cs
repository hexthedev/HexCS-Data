using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// <para>Used for any value. Values most commonly come after = signs, or are used as arguments in functions</para>
    /// <example>x = IValue, Func1(IValue);</example>
    /// </summary>
    public interface IValue : IGenerator
    {

    }
}
