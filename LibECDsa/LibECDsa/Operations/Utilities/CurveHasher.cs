using LibECDsa.Enums;
using System.Security.Cryptography;

namespace LibECDsa.Operations.Utilities
{
    /// <summary>
    /// Provides hashing functionality based on the elliptic curve parameters.
    /// </summary>
    internal class CurveHasher
    {
        private readonly EllipticCurve _curve;
        private readonly int _bitSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurveHasher"/> class with the specified elliptic curve.
        /// </summary>
        /// <param name="curve">The elliptic curve parameters to use for hashing.</param>
        public CurveHasher(EllipticCurve curve)
        {
            _curve = curve;
            _bitSize = _curve.BitSize;
        }

        /// <summary>
        /// Computes the hash of the provided input using the appropriate hashing algorithm based on the curve's bit size.
        /// </summary>
        /// <param name="input">The byte array to hash.</param>
        /// <returns>The computed hash as a byte array.</returns>
        /// <exception cref="NotSupportedException">Thrown when the curve's bit size is not supported.</exception>
        public byte[] ComputeHash(byte[] input)
        {
            using HashAlgorithm hashAlg = _bitSize switch
            {
                (int)CurveBitSize._192 => SHA1.Create(),
                //(int)CurveBitSize._224 => SHA224.Create(),
                (int)CurveBitSize._256 => SHA256.Create(),
                (int)CurveBitSize._384 => SHA384.Create(),
                (int)CurveBitSize._512 => SHA512.Create(),
                _ => throw new NotSupportedException("Unsupported curve type")
            };

            // Compute and return the hash of the input
            return hashAlg.ComputeHash(input);
        }
    }
}
