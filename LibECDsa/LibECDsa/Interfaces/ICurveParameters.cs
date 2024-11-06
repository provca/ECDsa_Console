using System.Numerics;

namespace LibECDsa.Interfaces
{
    /// <summary>
    /// Defines the parameters for an elliptic curve used in ECDSA.
    /// </summary>
    public interface ICurveParameters
    {
        /// <summary>
        /// Gets the name of the elliptic curve.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the prime number defining the finite field Fp.
        /// </summary>
        BigInteger P { get; }

        /// <summary>
        /// Gets the coefficient a of the elliptic curve equation.
        /// </summary>
        BigInteger A { get; }

        /// <summary>
        /// Gets the coefficient b of the elliptic curve equation.
        /// </summary>
        BigInteger B { get; }

        /// <summary>
        /// Gets the base point G on the elliptic curve.
        /// </summary>
        ECPoint G { get; }

        /// <summary>
        /// Gets the order of the base point G.
        /// </summary>
        BigInteger N { get; }

        /// <summary>
        /// Gets the bit size of the elliptic curve.
        /// </summary>
        int BitSize { get; }

        /// <summary>
        /// Gets the cofactor of the elliptic curve.
        /// </summary>
        int H { get; }

        /// <summary>
        /// Gets the seed value used for curve generation, if applicable.
        /// </summary>
        BigInteger? S { get; }
    }
}
