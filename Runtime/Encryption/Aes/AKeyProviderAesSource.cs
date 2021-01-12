namespace HexCS.Data.Encryption
{
    /// <summary>
    /// <para>Source key providers are used to hardcode keys into the dlls of an application.
    /// This provides a level of key management that is only suitable for info you don't want
    /// non-tech savvy users to get. If someone can unpack your dll, they can get the key. I would
    /// use this technique only with info which isn't worth the inconvenience to decrypt</para>
    /// </summary>
    public abstract class AKeyProviderAesSource : IKeyProviderAes
    {
        /// <inheritdoc/>
        public abstract byte[] Key { get; }

        /// <inheritdoc/>
        public abstract byte[] Iv { get; }
    }
}
