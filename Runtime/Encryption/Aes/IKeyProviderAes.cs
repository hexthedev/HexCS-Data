using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Encryption
{
    /// <summary>
    /// Provides key and iv to an Aes encryption class.
    /// </summary>
    public interface IKeyProviderAes
    {
        /// <summary>
        /// The key to be used during encryption and decryption
        /// </summary>
        byte[] Key { get; }

        /// <summary>
        /// The IV to be used during encryption and decryption
        /// </summary>
        byte[] Iv { get; }
    }
}
