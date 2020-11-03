using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Encryption
{
    /// <summary>
    /// Simple key provider
    /// </summary>
    public class KeyProviderAesSimple : IKeyProviderAes
    {
        /// <inheritdoc />
        public byte[] Key { get; set; }

        /// <inheritdoc />
        public byte[] Iv { get; set; }
    }
}
