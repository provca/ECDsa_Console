using LibECDsa.Interfaces;
using LibECDsa.Operations.Utilities;
using System.Numerics;
using System.Text;

namespace LibECDsa.Operations.Messages
{
    public class ECDsaVerifier
    {
        private readonly EllipticCurve _curve;  // The elliptic curve used for verification
        private readonly ECPoint _g;            // The base point G of the curve
        private readonly BigInteger _n;         // The order of the base point G
        private readonly BigInteger _h;         // The cofactor of the curve

        public ECDsaVerifier(ICurveParameters curveParameters, EllipticCurve curve)
        {
            _curve = curve;
            _g = curveParameters.G;
            _n = curveParameters.N;
            _h = curveParameters.H;
        }

        /// <summary>
        /// Verifies the ECDSA signature for the given message and public key.
        /// </summary>
        /// <param name="publicKey">The public key used for verification.</param>
        /// <param name="signature">The signature to verify.</param>
        /// <param name="message">The message that was signed.</param>
        /// <param name="encoding">Represents a character encoding format. UTF8 by default if null.</param>
        /// <returns>True if the signature is valid; otherwise, false.</returns>
        public bool VerifySignature(ECPoint publicKey, ECPoint signature, string message, Encoding? encoding = null)
        {
            // Set a default encoding if null.
            encoding ??= Encoding.UTF8;
            CurveHasher curveHasher = new(_curve);

            // Convert the message to bytes
            byte[] messageBytes = encoding.GetBytes(message);

            // Hash the message to get the value z
            BigInteger z = new(curveHasher.ComputeHash(messageBytes), isUnsigned: true, isBigEndian: true);

            // Calculate w as the modular inverse of the signature's Y component
            BigInteger w = EllipticCurve.ModularInverse(signature.Y, _n);

            // Compute the values u1 and u2 using the curve's multiplication and base point
            ECPoint u1 = _curve.Multiply(w * z * _h, _g);
            ECPoint u2 = _curve.Multiply(w * signature.X * _h, publicKey);

            // Add u1 and u2 to get the sum point
            ECPoint sum = _curve.Add(u1, u2);

            // The signature is valid if the X coordinate of the sum equals the signature's X coordinate
            return sum.X == signature.X;
        }
    }
}
