using System.Numerics;

namespace LibECDsa.Operations.PublicKeys
{
    /// <summary>
    /// Provides functionality to serialize public keys for elliptic curve cryptography.
    /// </summary>
    public class PublicKeySerializer
    {
        private readonly EllipticCurve _curve;
        private static int _byteLength;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublicKeySerializer"/> class with the specified elliptic curve.
        /// </summary>
        /// <param name="curve">The elliptic curve used for public key operations.</param>
        public PublicKeySerializer(EllipticCurve curve)
        {
            _curve = curve;
            _byteLength = _curve.BitSize / 8;
        }

        /// <summary>
        /// Generates public key representations from the given private key.
        /// </summary>
        /// <param name="privateKey">The private key used to compute the public key point.</param>
        /// <returns>A <see cref="PublicKeyRepresentation"/> containing serialized public key formats.</returns>
        public PublicKeyRepresentation GetPublicKeyRepresentations(BigInteger privateKey)
        {
            ECPoint? publicKeyPoint = _curve.Multiply(privateKey, _curve.G);

            byte[] uncompressedKey = SerializeUncompressedPoint(publicKeyPoint);
            byte[] compressedKey = SerializeCompressedPoint(publicKeyPoint);

            return new PublicKeyRepresentation(uncompressedKey, compressedKey, publicKeyPoint.X, publicKeyPoint.Y);
        }

        /// <summary>
        /// Serializes a given elliptic curve point in a compressed format.
        /// </summary>
        /// <param name="point">The elliptic curve point to serialize.</param>
        /// <returns>A byte array representing the compressed point.</returns>
        private static byte[] SerializeCompressedPoint(ECPoint point)
        {
            // Determine the prefix based on the parity of the Y coordinate
            byte prefix = (byte)(point.Y % 2 == 0 ? 0x02 : 0x03);

            // Convert the X coordinate to a byte array
            byte[] xBytes = point.X.ToByteArray(isUnsigned: true, isBigEndian: true);

            // Adjust the length of the X byte array to match the expected byte length
            xBytes = AdjustArrayLength(xBytes, _byteLength);

            // Return a concatenated array of the prefix and the adjusted X bytes
            return new byte[] { prefix }.Concat(xBytes).ToArray();
        }

        /// <summary>
        /// Serializes a given elliptic curve point in an uncompressed format.
        /// </summary>
        /// <param name="point">The elliptic curve point to serialize.</param>
        /// <returns>A byte array representing the uncompressed point.</returns>
        private static byte[] SerializeUncompressedPoint(ECPoint point)
        {
            // Convert the X and Y coordinates to byte arrays
            byte[] xBytes = point.X.ToByteArray(isUnsigned: true, isBigEndian: true);
            byte[] yBytes = point.Y.ToByteArray(isUnsigned: true, isBigEndian: true);

            // Adjust the length of the X and Y byte arrays to match the expected byte length
            xBytes = AdjustArrayLength(xBytes, _byteLength);
            yBytes = AdjustArrayLength(yBytes, _byteLength);

            // Return a concatenated array with the uncompressed prefix (0x04), adjusted X, and adjusted Y bytes
            return new byte[] { 0x04 }.Concat(xBytes).Concat(yBytes).ToArray();
        }

        /// <summary>
        /// Adjusts the length of a byte array to the specified length by adding leading zeros if necessary.
        /// </summary>
        /// <param name="input">The byte array to adjust.</param>
        /// <param name="length">The desired length of the output byte array.</param>
        /// <returns>A new byte array of the specified length.</returns>
        private static byte[] AdjustArrayLength(byte[] input, int length)
        {
            if (input.Length == length)
                return input; // Return the input array if it is already the correct length

            // Create a new array of the specified length
            byte[] adjusted = new byte[length];

            // Copy the original array into the new array, aligning it to the right
            Array.Copy(input, 0, adjusted, length - input.Length, input.Length);

            // Return the adjusted array.
            return adjusted;
        }
    }
}
