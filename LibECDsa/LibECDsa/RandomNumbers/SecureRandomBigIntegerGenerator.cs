using LibECDsa.Operations;
using LibECDsa.Operations.Utilities;
using System.Numerics;
using System.Security.Cryptography;

namespace LibECDsa.RandomNumbers
{
    /// <summary>
    /// Generates cryptographically secure random BigIntegers, with optional constraints.
    /// </summary>
    public class SecureRandomBigIntegerGenerator
    {
        // The elliptic curve used for random number generation (optional).
        private readonly EllipticCurve? _curve;

        // Verifier for validating elliptic curve parameters.
        private readonly CurveVerifier? _verifier;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecureRandomBigIntegerGenerator"/> class
        /// with the specified elliptic curve and verification parameters.
        /// </summary>
        /// <param name="curve">The elliptic curve to use for random number generation (optional).</param>
        public SecureRandomBigIntegerGenerator(EllipticCurve? curve = null)
        {
            _curve = curve;

            // Initialize the verifier if the curve is provided.
            _verifier = curve != null ? new CurveVerifier(curve) : null;
        }

        /// <summary>
        /// Generates a cryptographically secure random BigInteger that is less than a specified maximum value from a selected curve.
        /// </summary>
        /// <param name="maxExclusive">The exclusive upper bound for the generated value. Must be greater than 1.</param>
        /// <returns>A random BigInteger between 1 (inclusive) and <paramref name="maxExclusive"/> (exclusive).</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="maxExclusive"/> is less than or equal to 1.</exception>
        public BigInteger GenerateRandomBigIntegerForCurve(BigInteger maxExclusive)
        {
            if (maxExclusive <= 1)
            {
                throw new ArgumentException("The maximum value must be greater than 1.", nameof(maxExclusive));
            }

            // Ensure the curve parameters are valid for random generation.
            if (_curve == null || _verifier == null || !_verifier.IsCurveParametersValid(_curve.A, _curve.B, _curve.P, _curve.G, _curve.N, _curve.S))
            {
                throw new InvalidOperationException("Valid elliptic curve is required for curve-based random generation.");
            }

            BigInteger result;

            do
            {
                // Generate a random BigInteger.
                result = GenerateDRBGRandomBigInteger();

                // Verify that the generated number is within the range.
                if (result < 1 || result >= maxExclusive)
                {
                    Console.WriteLine($"Random number out of range: {result}");
                }

            } while (result < 1 || result >= maxExclusive);

            // Return the valid random BigInteger.
            return result;
        }

        /// <summary>
        /// Generates a cryptographically secure BigInteger using Deterministic Random Bit Generators (DRBG).
        /// </summary>
        /// <returns>A securely generated random BigInteger.</returns>
        private BigInteger GenerateDRBGRandomBigInteger()
        {
            if (_curve == null)
                throw new InvalidOperationException("The elliptic curve is not initialized.");

            // Calculate the byte length based on the curve bit size.
            int byteLength = (_curve.BitSize + 7) / 8;
            byte[] bytes = new byte[byteLength];

            // Fill the byte array with cryptographically secure random bytes.
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }

            // Adjust the most significant bits to match the curve's bit size.
            bytes[^1] &= (byte)(0xFF >> byteLength * 8 - _curve.BitSize);

            // Hash the bytes to further randomize them based on curve parameters.
            CurveHasher curveHasher = new(_curve);
            bytes = curveHasher.ComputeHash(bytes);

            // Convert the byte array to a BigInteger and return.
            return new BigInteger(bytes, isUnsigned: true, isBigEndian: true);
        }
    }
}
