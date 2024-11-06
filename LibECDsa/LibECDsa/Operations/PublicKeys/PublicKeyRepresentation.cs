using LibECDsa.Operations.Utilities;
using System.Numerics;

namespace LibECDsa.Operations.PublicKeys
{
    public class PublicKeyRepresentation
    {
        /// <summary>
        /// Property to hold the uncompressed public key format as byte[].
        /// </summary>
        public byte[] Uncompressed { get; }

        /// <summary>
        /// Property to hold the compressed public key format as byte[].
        /// </summary>
        public byte[] Compressed { get; }

        /// <summary>
        /// Property for the X coordinate of the public key point as BigInteger.
        /// </summary>
        public BigInteger X { get; }

        /// <summary>
        /// Property for the Y coordinate of the public key point as BigInteger.
        /// </summary>
        public BigInteger Y { get; }

        /// <summary>
        /// Base64 representation without delimiters PEM.
        /// </summary>
        public string PublicKeyBase64 => NumeralSystem.ToBase64(Uncompressed);

        /// <summary>
        /// PEM representation of the public key.
        /// </summary>
        public string PublicKeyPemBase64 => NumeralSystem.PEM(Uncompressed);

        /// <summary>
        /// Property to display the public key in DER format.
        /// </summary>
        public string PublicKeyDerBase64 => NumeralSystem.DerBase64(X, Y);

        /// <summary>
        /// Hexadecimal representation without prefix and leading zeros in X and Y.
        /// </summary>
        public string Hexadecimal =>
            $"{NumeralSystem.RemoveLeadingZero(X.ToString("X"))}" +
            $"{NumeralSystem.RemoveLeadingZero(Y.ToString("X"))}";

        /// <summary>
        /// Octal representation without prefix and leading zeros in X and Y.
        /// </summary>
        public string Octal =>
            $"{NumeralSystem.RemoveLeadingZero(
                NumeralSystem.ConvertBigIntegerToOctal(X))}" +
            $"{NumeralSystem.RemoveLeadingZero(
                NumeralSystem.ConvertBigIntegerToOctal(Y))}";

        /// <summary>
        /// Decimal representation without prefix and leading zeros in X and Y.
        /// </summary>
        public string Decimal =>
            $"{NumeralSystem.RemoveLeadingZero(X.ToString())}" +
            $"{NumeralSystem.RemoveLeadingZero(Y.ToString())}";

        public PublicKeyRepresentation(byte[] uncompressed, byte[] compressed, BigInteger x, BigInteger y)
        {
            Uncompressed = uncompressed;
            Compressed = compressed;
            X = x;
            Y = y;
        }
    }
}

