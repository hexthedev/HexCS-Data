using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace HexCS.Data.Serialization
{
    /// <summary>
    /// Serialization utilities, mstly for conerting to and from
    /// byte[]s
    /// </summary>
    public static class UTSerialization
    {
        /// <summary>
        /// Uses a binary formatter to serailize target object to byte[]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        public static byte[] BinarySerialize<T>(T target)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, target);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// Uses a binary formatter to deserialize a byte[] to object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static T BinaryDeserialize<T>(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                return (T)new BinaryFormatter().Deserialize(stream);
            }
        }

        /// <summary>
        /// Deserializes an object of the ISimpleSerialiable interface
        /// using a byte[]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static T DeserializeSimple<T>(this byte[] bytes) where T : ISimpleSerializable, new()
        {
            T instance = new T();
            instance.ConstructFromBytes(bytes);
            return instance;
        }

        /// <summary>
        /// Converts an int[] array to bytes
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        // T-DO: Add for other types of arrays 
        public static byte[] ToBytes(this int[] array)
        {
            byte[] buffer = new byte[array.Length * sizeof(int)];
            Buffer.BlockCopy(array, 0, buffer, 0, buffer.Length);
            return buffer;
        }

        /// <summary>
        /// Attempts to convert the byte[] into an int[]
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static int[] AsIntArray(this byte[] buffer)
        {
            int[] array = new int[buffer.Length / sizeof(int)];
            Buffer.BlockCopy(buffer, 0, array, 0, buffer.Length);
            return array;
        }
    }
}
