using System.Numerics;
using System.Security.Cryptography;

namespace LibECDsa.Operations.Utilities
{
    /// <summary>
    /// Provides verification functionality for elliptic curve parameters.
    /// </summary>
    public class CurveVerifier
    {
        private readonly EllipticCurve _curve;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurveVerifier"/> class with the specified elliptic curve.
        /// </summary>
        /// <param name="curve">The elliptic curve parameters to verify.</param>
        public CurveVerifier(EllipticCurve curve)
        {
            _curve = curve;
        }

        /// <summary>
        /// Verifies the validity of the curve parameters according to ANSI X9.62 standards.
        /// </summary>
        /// <param name="a">Coefficient a of the curve equation.</param>
        /// <param name="b">Coefficient b of the curve equation.</param>
        /// <param name="P">Prime number defining the finite field Fp.</param>
        /// <param name="G">Base point on the curve.</param>
        /// <param name="n">Order of the base point G.</param>
        /// <param name="seed">Optional seed parameter.</param>
        /// <returns>True if all curve parameters are valid; otherwise, false.</returns>
        public bool IsCurveParametersValid(BigInteger a, BigInteger b, BigInteger P, ECPoint G, BigInteger n, BigInteger? seed)
        {
            // Validation 1: Non-singularity check
            if (!IsNonSingular(a, b, P))
            {
                Console.WriteLine("The curve is singular and not secure.");
                return false;
            }

            // Validation 2: Validity of parameter b in Fp
            if (b.Sign < 0 || b >= P)
            {
                Console.WriteLine("Parameter b is not valid in the field Fp.");
                return false;
            }

            // Validation 3: Validity of the seed parameter, if present
            if (seed.HasValue && !IsSeedValid(seed.Value, b, P))
            {
                Console.WriteLine("The seed parameter did not generate a valid value for b.");
                return false;
            }

            // Validation 4: Base point G on the curve
            if (!IsPointOnCurve(G, a, b, P))
            {
                Console.WriteLine("The base point G is not on the curve.");
                return false;
            }

            // Validation 5: Validity of order n of point G
            if (!IsValidOrder(G, n))
            {
                Console.WriteLine("The order n of point G is not valid.");
                return false;
            }

            Console.WriteLine("All curve parameters are valid.");
            return true;
        }

        /// <summary>
        /// Checks if the elliptic curve defined by parameters a and b is non-singular.
        /// </summary>
        /// <param name="a">The coefficient a in the elliptic curve equation.</param>
        /// <param name="b">The coefficient b in the elliptic curve equation.</param>
        /// <param name="P">The prime field modulus.</param>
        /// <returns>True if the curve is non-singular; otherwise, false.</returns>
        private static bool IsNonSingular(BigInteger a, BigInteger b, BigInteger P)
        {
            // Check that the curve is non-singular: 4*a^3 + 27*b^2 ≠ 0 mod p
            BigInteger nonSingularityCheck = (4 * BigInteger.Pow(a, 3) + 27 * BigInteger.Pow(b, 2)) % P;
            return nonSingularityCheck != 0;
        }

        /// <summary>
        /// Validates the given seed against the coefficient b using a hash function.
        /// </summary>
        /// <param name="seed">The seed to validate.</param>
        /// <param name="b">The expected value of b from the curve parameters.</param>
        /// <param name="P">The prime field modulus.</param>
        /// <returns>True if the derived value from the seed matches b; otherwise, false.</returns>
        private static bool IsSeedValid(BigInteger seed, BigInteger b, BigInteger P)
        {
            using (SHA1 sha = SHA1.Create())
            {
                byte[] seedBytes = seed.ToByteArray();
                byte[] hash = sha.ComputeHash(seedBytes);
                BigInteger derivedB = new BigInteger(hash) % P;
                return derivedB == b;
            }
        }

        /// <summary>
        /// Checks if a point lies on the elliptic curve defined by coefficients a, b, and the prime modulus P.
        /// </summary>
        /// <param name="point">The point to check.</param>
        /// <param name="a">The coefficient a in the elliptic curve equation.</param>
        /// <param name="b">The coefficient b in the elliptic curve equation.</param>
        /// <param name="P">The prime field modulus.</param>
        /// <returns>True if the point is on the curve; otherwise, false.</returns>
        private static bool IsPointOnCurve(ECPoint point, BigInteger a, BigInteger b, BigInteger P)
        {
            BigInteger leftSide = BigInteger.ModPow(point.Y, 2, P);
            BigInteger rightSide = (BigInteger.ModPow(point.X, 3, P) + a * point.X + b) % P;
            return leftSide == rightSide;
        }

        /// <summary>
        /// Validates that the order n of the point G is correct.
        /// </summary>
        /// <param name="G">The point G whose order is to be checked.</param>
        /// <param name="n">The expected order of the point G.</param>
        /// <returns>True if the order is valid; otherwise, false.</returns>
        private bool IsValidOrder(ECPoint G, BigInteger n)
        {
            // Check that n is a valid and non-trivial value
            if (n <= 0)
            {
                Console.WriteLine("The order n is not valid.");
                return false;
            }

            // Multiply point G by n and verify if the result is the point at infinity
            ECPoint result = _curve.Multiply(n, G);

            // The result must be the point at infinity if n is the order of G
            bool isValidOrder = result == EllipticCurve.Infinity;

            if (!isValidOrder)
            {
                Console.WriteLine("Point G does not have the specified order.");
            }

            return isValidOrder;
        }
    }
}
