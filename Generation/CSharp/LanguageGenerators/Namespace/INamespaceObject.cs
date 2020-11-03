using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// Generates a namespace object. This is any object that can live in a namespace.
    /// i.e. class, interface, enum, etc. 
    /// </summary>
    public interface INamespaceObject : IGenerator
    {
    }
}
