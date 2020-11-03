using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using HexCS.Core;
using HexCS.Data.Serialization;

namespace HexCS.Data.Encryption
{
    /// <summary>
    /// Object capable of encrypting and decrypting using AES encrption
    /// </summary>
    public class EncryptorAes : IEncryptor
    {
        #region Api
        /// <summary>
        /// Provides the key and iv required to encrypt/decrypt.
        /// </summary>
        public IKeyProviderAes KeyProvider { get; set; }

        /// <summary>
        /// Create an AEncryptorAes class
        /// </summary>
        /// <param name="keyProvider"></param>
        public EncryptorAes(IKeyProviderAes keyProvider)
        {
            KeyProvider = keyProvider ?? throw new ArgumentException("keyProvider cannot be null");
        }

        /// <summary>
        /// Decrypt a buffer using Aes then deserialize buffer to object T. 
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns></returns>
        public byte[] Decrypt(byte[] cipher)
        {
            byte[] decryptBuffer;

            // In all examples I see the AesManaged needs disposing after use. Not
            // sure how true this is though. I will do it until I need to squeeze more
            // performance in which case I will cache it
            using (AesManaged aes = UTAesManaged.FromKeyProvider(KeyProvider))
            {
                ICryptoTransform decryptor = aes.CreateDecryptor();

                using (MemoryStream mem = new MemoryStream(cipher))
                {
                    using (CryptoStream crypt = new CryptoStream(mem, decryptor, CryptoStreamMode.Read))
                    {
                        decryptBuffer = crypt.ReadAllBytes();
                    }
                }
            }

            return decryptBuffer;
        }

        /// <summary>
        /// Encrpyt an object using the AES method
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public byte[] Encrypt(byte[] obj)
        {
            using (AesManaged aes = UTAesManaged.FromKeyProvider(KeyProvider))
            {
                ICryptoTransform encryptor = aes.CreateEncryptor();

                using (MemoryStream mem = new MemoryStream(obj))
                {
                    using (CryptoStream crypt = new CryptoStream(mem, encryptor, CryptoStreamMode.Read))
                    {
                        return crypt.ReadAllBytes();
                    }
                }
            }
        }
        #endregion
    }
}
