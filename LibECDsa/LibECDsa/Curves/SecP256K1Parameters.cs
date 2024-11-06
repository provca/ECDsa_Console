using LibECDsa.Enums;
using LibECDsa.Interfaces;
using LibECDsa.Operations.Utilities;
using System.Numerics;

namespace LibECDsa.Curves
{
    /// <summary>
    /// Parameters for the secp256k1 elliptic curve.
    /// </summary>
    /// <remarks>
    /// This elliptic curve is defined over the prime field P, where:
    /// <para>
    ///     y^2 = x^3 + ax + b, with a = 0 and b = 7.
    /// </para>
    /// <para>
    ///     P = 2^256 - 2^32 - 977.
    /// </para>
    /// <para>
    ///     The base point G has an order n that is a prime number.
    /// </para>
    /// </remarks>
    public class SecP256K1Parameters : ICurveParameters
    {
        /// <inheritdoc />
        public string Name => nameof(CurveType.secp256k1);

        /// <inheritdoc />
        public BigInteger P => new(BitsFlow.GetHexBytes("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFC2F").Reverse().ToArray());

        /// <inheritdoc />
        public BigInteger A => BigInteger.Zero;

        /// <inheritdoc />
        public BigInteger B => 7;

        /// <inheritdoc />
        public ECPoint G => new(
            new(BitsFlow.GetHexBytes("79BE667EF9DCBBAC55A06295CE870B07029BFCDB2DCE28D959F2815B16F81798").Reverse().ToArray()),
            new(BitsFlow.GetHexBytes("483ADA7726A3C4655DA4FBFC0E1108A8FD17B448A68554199C47D08FFB10D4B8").Reverse().ToArray())
        );

        /// <inheritdoc />
        public BigInteger N => new(BitsFlow.GetHexBytes("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEBAAEDCE6AF48A03BBFD25E8CD0364141").Reverse().ToArray());

        /// <inheritdoc />
        public int BitSize => (int)CurveBitSize._256;

        /// <inheritdoc />
        public int H => 1;

        /// <inheritdoc />
        public BigInteger? S => null;
    }
}
