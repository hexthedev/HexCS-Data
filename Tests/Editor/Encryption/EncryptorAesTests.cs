using NUnit.Framework;
using System.Security.Cryptography;
using HexCS.Core;
using HexCS.Data.Encryption;
using HexCS.Data.Serialization;

namespace HexCSTests.Data
{
    [TestFixture]
    public class EncryptorAesTests
    {
        [Test]
        public void Works()
        {
            // Arrange
            AesManaged aes = new AesManaged();
            aes.GenerateKey();
            aes.GenerateIV();

            KeyProviderAesSimple kp = new KeyProviderAesSimple()
            {
                Key = aes.Key,
                Iv = aes.IV
            };

            // This tests a case in the ReadAllBytes function also, thats why I chose 9001
            int[] test = UTArray.ConstructArray(9001, () => UTRandom.Int(int.MaxValue));
            ExampleSerializable ser = new ExampleSerializable(test);

            // Act
            EncryptorAes encryptor = new EncryptorAes(kp);

            byte[] encrypted = encryptor.Encrypt(ser.GetBytes());
            byte[] not_encrypted = ser.GetBytes();
            bool encryptionChangedBytes = !encrypted.EqualsElementWise(not_encrypted);

            ExampleSerializable decrypted = new ExampleSerializable();
            decrypted.ConstructFromBytes(encryptor.Decrypt(encrypted));
            bool decryptedCorrectly = ser.Value.EqualsElementWise(decrypted.Value);

            // Assert
            Assert.That(encryptionChangedBytes);
            Assert.That(decryptedCorrectly);
        }

        /// <summary>
        /// Simple ISimpleSerializable class
        /// </summary>
        private class ExampleSerializable : ISimpleSerializable
        {
            /// <summary>
            /// Example Value
            /// </summary>
            public int[] Value;

            /// <summary>
            /// Required for deserialization
            /// </summary>
            public ExampleSerializable() { }

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="val"></param>
            public ExampleSerializable(int[] val)
            {
                Value = val;
            }

            /// <inheritdoc />
            public void ConstructFromBytes(byte[] bytes) => Value = bytes.AsIntArray();

            /// <inheritdoc />
            public byte[] GetBytes() => Value.ToBytes();
        }
    }
}