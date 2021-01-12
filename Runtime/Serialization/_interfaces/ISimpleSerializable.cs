namespace HexCS.Data.Serialization
{
    /// <summary>
    /// <para>The .NET Binary serializer has a lot of features that are
    /// resulting in code bulk. I need simple, featureless, lightweight
    /// binary serialization that can be used to send simple messages.</para>
    /// 
    /// <para>To do this, I am creating a SimpleSerializable inferface that
    /// exposes the GetBytes() and ConstructFromBytes() functions</para>
    /// 
    /// <para>In order to work, objects inheriting ISimpleSerializable
    /// require a public default constructor, otherwise the instance
    /// that is created will be completely override when calling
    /// ConstructFromBytes()</para>
    /// </summary>
    public interface ISimpleSerializable
    {
        /// <summary>
        /// Use bytes to construct the class.
        /// Should be called on a class created from
        /// a default constructor
        /// </summary>
        void ConstructFromBytes(byte[] bytes);

        /// <summary>
        /// Return this class as a byte[], which
        /// is a simple byte format for the class
        /// </summary>
        /// <returns></returns>
        byte[] GetBytes();
    }
}
