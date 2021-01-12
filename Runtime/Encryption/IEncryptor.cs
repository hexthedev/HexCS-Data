using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Encryption
{
    /// <summary>
    /// Capable of taking some object of type T and changing it to an encryted byte[].
    /// It can also take a byte[] and decrypt it
    /// </summary>
    public interface IEncryptor
    {
        /// <summary>
        /// Encrypt the object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        byte[] Encrypt(byte[] obj);

        /// <summary>
        /// Decrypt the buffer into an object
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        byte[] Decrypt(byte[] buffer);
    }
}
