using System;
using System.Collections.Generic;
using System.Text;
using HexCS.Data.Runtime;
using HexCS.Data.Generation.Yaml;

namespace HexCS.Data.Serialization
{
    /// <summary>
    /// Can Serialize and Deserialize YAML
    /// </summary>
    public class SerializerYaml : ISyntaxSerializer
    {
        private StringBuilder _sb = new StringBuilder();
        private Encoding _encoding = Encoding.UTF8;

        public ESerializationFormat Format { get; set; } = ESerializationFormat.Human;

        public ESerializationSyntax Syntax => ESerializationSyntax.YAML;

        /// <inheritdoc />
        public IData Deserialize<T>(byte[] data) where T : IData
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public byte[] Serialize(IData data)
        {
            _sb.Clear();
            using (GTIDataFile f = new GTIDataFile(_sb, data)) { }
            return _encoding.GetBytes(_sb.ToString());
        }
    }
}
