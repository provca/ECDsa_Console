using LibECDsa.Operations.Utilities;
using LibECDsa.RandomNumbers;
using System.Numerics;

namespace LibECDsa.Operations.Messages
{
    public class ECDsaSigner
    {
        private readonly EllipticCurve _curve;      // The elliptic curve used for signing
        private readonly BigInteger _n;             // Order of the curve's base point
        private readonly CurveVerifier _verifier;   // Verifier for curve parameters

        public ECDsaSigner(EllipticCurve curve, BigInteger n)
        {
            _curve = curve;
            _n = n;
            _verifier = new CurveVerifier(curve);
        }

        /// <summary>
        /// Signs a message using the given private key.
        /// </summary>
        /// <param name="message">The message to sign.</param>
        /// <param name="privateKey">The private key used for signing.</param>
        /// <returns>An ECPoint representing the signature (R, s).</returns>
        public ECPoint SignMessage(string message, BigInteger privateKey)
        {
            // Validate the curve parameters before signing
            if (!_verifier.IsCurveParametersValid(
                _curve.A,
                _curve.B,
                _curve.P,
                _curve.G,
                _curve.N,
                _curve.S))
            {
                throw new NotSupportedException("Unsupported curve type to sign the message.");
            }

            // Convert the message to bytes
            byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(message);

            // Hash the message to get the value z
            CurveHasher curveHasher = new(_curve);
            BigInteger z = new(curveHasher.ComputeHash(messageBytes), isUnsigned: true, isBigEndian: true);

            // Generate a secure random integer k
            SecureRandomBigIntegerGenerator sRBIG = new(_curve);
            BigInteger k = sRBIG.GenerateRandomBigIntegerForCurve(_n);

            // Compute the point R = k * G
            ECPoint R = _curve.Multiply(k, _curve.G);
            if (R == EllipticCurve.Infinity)
            {
                Console.WriteLine("R is Infinity after multiplying k with G.");
                return SignMessage(message, privateKey);
            }

            // Calculate the signature component s
            BigInteger s = EllipticCurve.ModularInverse(k, _n) * (z + R.X * privateKey) % _n;

            // Return the signature as an ECPoint (R.X, s)
            return new ECPoint(R.X, s);
        }
    }
}
