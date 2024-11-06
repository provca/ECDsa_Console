using LibECDsa.Interfaces;
using System.Numerics;

namespace LibECDsa.Operations
{
    /// <summary>
    /// Represents an elliptic curve, supporting cryptographic operations such as point addition, doubling, and scalar multiplication.
    /// </summary>
    public class EllipticCurve : ICurveParameters
    {
        private readonly ICurveParameters _curveParameters;

        /// <summary>
        /// Initializes an instance of the <see cref="EllipticCurve"/> class using the specified curve parameters.
        /// </summary>
        /// <param name="curveParameters">Parameters of the elliptic curve.</param>
        public EllipticCurve(ICurveParameters curveParameters)
        {
            _curveParameters = curveParameters;
        }

        /// <inheritdoc />
        public string Name => _curveParameters.Name;

        /// <inheritdoc />
        public BigInteger P => _curveParameters.P;

        /// <inheritdoc />
        public BigInteger A => _curveParameters.A;

        /// <inheritdoc />
        public BigInteger B => _curveParameters.B;

        /// <inheritdoc />
        public ECPoint G => _curveParameters.G;

        /// <inheritdoc />
        public BigInteger N => _curveParameters.N;

        /// <inheritdoc />
        public int BitSize => _curveParameters.BitSize;

        /// <inheritdoc />
        public int H => _curveParameters.H;

        /// <inheritdoc />
        public BigInteger? S => _curveParameters.S;

        /// <summary>
        /// Represents the point at infinity on the elliptic curve.
        /// </summary>
        public static ECPoint? Infinity => null;

        /// <summary>
        /// Computes the modular inverse of a value modulo a specified modulus.
        /// </summary>
        /// <param name="value">The value for which to compute the modular inverse.</param>
        /// <param name="mod">The modulus.</param>
        /// <returns>The modular inverse of <paramref name="value"/> modulo <paramref name="mod"/>.</returns>
        public static BigInteger ModularInverse(BigInteger value, BigInteger mod)
        {
            return BigInteger.ModPow(value, mod - 2, mod);
        }

        /// <summary>
        /// Doubles a point on the elliptic curve.
        /// </summary>
        /// <param name="point">The point to double.</param>
        /// <returns>The result of doubling <paramref name="point"/>, or <see cref="Infinity"/> if the point is the point at infinity.</returns>
        public ECPoint? Double(ECPoint point)
        {
            // ToDo
            // Selected curves to Check for point at infinity and other conditions where doubling results in infinity:
            // secp112r1, secp112r2, secp128r1, secp128r2, secp160r1, secp160k1, secp192k1, secp256k1, secp384r1, secp521r1, secp224k1

            if (point == null) return null;

            // Calculate the slope (3x^2 + a) / (2y) mod p
            BigInteger slope = (3 * BigInteger.Pow(point.X, 2) + A) * ModularInverse(2 * point.Y, P) % P;
            BigInteger xR = (BigInteger.Pow(slope, 2) - 2 * point.X) % P;
            BigInteger yR = (slope * (point.X - xR) - point.Y) % P;

            return new ECPoint(xR < 0 ? xR + P : xR, yR < 0 ? yR + P : yR);
        }

        /// <summary>
        /// Adds two points on the elliptic curve.
        /// </summary>
        /// <param name="pt1">The first point.</param>
        /// <param name="pt2">The second point.</param>
        /// <returns>The result of adding <paramref name="pt1"/> and <paramref name="pt2"/>, or <see cref="Infinity"/> if the points are inverses.</returns>
        public ECPoint? Add(ECPoint pt1, ECPoint pt2)
        {
            if (pt1 == Infinity) return pt2;
            if (pt2 == Infinity) return pt1;
            if (pt1 == null || pt2 == null) return null;

            // If points have same x but different y, result is the point at infinity
            if (pt1.X == pt2.X && pt1.Y != pt2.Y) return Infinity;

            // If points are the same, perform doubling
            if (pt1.X == pt2.X && pt1.Y == pt2.Y) return Double(pt1);

            // Calculate the slope and result coordinates
            BigInteger slope = (pt2.Y - pt1.Y) * ModularInverse(pt2.X - pt1.X, P) % P;
            BigInteger xR = (BigInteger.Pow(slope, 2) - pt1.X - pt2.X) % P;
            BigInteger yR = (slope * (pt1.X - xR) - pt1.Y) % P;

            return new ECPoint(xR < 0 ? xR + P : xR, yR < 0 ? yR + P : yR);
        }

        /// <summary>
        /// Multiplies a point on the curve by a scalar.
        /// </summary>
        /// <param name="k">The scalar value.</param>
        /// <param name="point">The point to multiply, or <see cref="ICurveParameters.G"/> if <c>null</c>.</param>
        /// <returns>The result of scalar multiplication.</returns>
        public ECPoint? Multiply(BigInteger k, ECPoint? point = null)
        {
            if (point == null || k.IsZero)
                return Infinity;

            ECPoint? result = Infinity;
            ECPoint? current = point;

            while (k > 0)
            {
                if ((k & 1) == 1)
                {
                    if (result == Infinity)
                    {
                        result = current;
                    }
                    else
                    {
                        result = Add(result, current);
                    }
                }
                current = Double(current);
                k >>= 1;
            }

            return result;
        }
    }
}
