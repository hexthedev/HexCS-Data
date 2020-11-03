using System;
using System.Collections.Generic;
using System.Text;
using HexCS.Core;
using HexCS.Data.Runtime;

namespace HexCS.Data.Serialization
{
    /// <summary>
    /// Serialization managment class that handles serialization to multiple different types
    /// based on a common intermediate type. Set the Format and Type to customize the way
    /// serialization is performed. All string serialization is utf8.
    /// </summary>
    public class Serializer : IGenericSerializer
    {
        private const byte cXmlIndicatorCharacter = 0x3c; // utf8 '<' character, xml starts with <?xml version="1.0" encoding="utf-8"?>
        private const byte cJsonIndicatorCharacter = 0x7b; // utf8 '{' character, Json starts with {/n
        private const byte cYamlIndicatorCharacter = 0x25; // utf8 '%' character, Yaml starts with %YAML 1.2

        private EnumDicitonary<ESerializationSyntax, ISyntaxSerializer> _serializers
            = new EnumDicitonary<ESerializationSyntax, ISyntaxSerializer>();

        /// <inheritdoc/>
        public ESerializationFormat Format { get ; set; }

        /// <inheritdoc/>
        public ESerializationSyntax Syntax { get; private set; }

        /// <inheritdoc/>
        public byte[] Serialize(IData data) => _serializers[Syntax]?.Serialize(data);

        /// <inheritdoc/>
        public IData Deserialize<T>(byte[] data) where T:IData
        {
            if (data == null || data.Length == 0) return null;

            // Use the first byte (assumed UTF-8) to determine the serialized file syntax
            switch (data[0])
            {
                case cXmlIndicatorCharacter: return _serializers[ESerializationSyntax.XML]?.Deserialize<T>(data);
                case cJsonIndicatorCharacter: return _serializers[ESerializationSyntax.JSON]?.Deserialize<T>(data);
                case cYamlIndicatorCharacter: return _serializers[ESerializationSyntax.YAML]?.Deserialize<T>(data);
            }

            return _serializers[ESerializationSyntax.Binary]?.Deserialize<T>(data);
        }

        /// <inheritdoc/>
        public void SetSyntax(ESerializationSyntax syntax) => Syntax = syntax;
    }
}
