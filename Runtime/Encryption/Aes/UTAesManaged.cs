using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace HexCS.Data.Encryption
{
    /// <summary>
    /// Helpers and extensions for the UTAesManaged type
    /// </summary>
    public static class UTAesManaged
    {
        /// <summary>
        /// Constructs a new AesManaged from a keyProvider
        /// </summary>
        /// <param name="keyProvider"></param>
        /// <returns></returns>
        public static AesManaged FromKeyProvider(IKeyProviderAes keyProvider)
        {
            AesManaged enc = new AesManaged();
            enc.Key = keyProvider.Key;
            enc.IV = keyProvider.Iv;
            return enc;
        }
    }
}
