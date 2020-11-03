using HexCS.Data.Runtime;

namespace HexCS.Data.Parsing
{
    /// <summary>
    /// Used to parse some string into a KeyValuePair[string, object][]
    /// type. This is used to traslate between parse types into C# objects. 
    /// </summary>
    public interface IDataParser : IParser<InterObject>
    {
    }
}
