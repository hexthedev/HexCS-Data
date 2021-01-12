using System.Security.Cryptography;
using HexCS.Data.Encryption;

namespace HexCSTests.Data
{
    public static class EncryptionHelpers
    {
        public static EncryptorAes MakeEncryptor()
        {
            AesManaged aes = new AesManaged();
            aes.GenerateKey();
            aes.GenerateIV();

            KeyProviderAesSimple kp = new KeyProviderAesSimple()
            {
                Key = aes.Key,
                Iv = aes.IV
            };

           return new EncryptorAes(kp);
        }
    }
}
