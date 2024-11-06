using LibECDsa;
using LibECDsa.Interfaces;
using LibECDsa.Operations;
using LibECDsa.Operations.Messages;
using System.Numerics;

namespace LibServices.Security.Cryptography.ECDsaService.Messages
{
    /// <summary>
    /// Provides services for verifying ECDSA signatures.
    /// </summary>
    public class Service_ECDsaVerifier
    {
        private readonly ECDsaVerifier _verifier;

        /// <summary>
        /// Initializes a new instance of the <see cref="Service_ECDsaVerifier"/> class with the specified curve parameters and elliptic curve.
        /// </summary>
        /// <param name="curveParameters">The parameters of the elliptic curve used for signature verification.</param>
        /// <param name="curve">The elliptic curve instance associated with the curve parameters.</param>
        public Service_ECDsaVerifier(ICurveParameters curveParameters, EllipticCurve curve)
        {
            _verifier = new ECDsaVerifier(curveParameters, curve);
        }

        /// <summary>
        /// Verifies an ECDSA signature for a given message using the provided public key coordinates and signature components.
        /// </summary>
        /// <param name="message">The original message that was signed.</param>
        /// <param name="publicKeyX">The X coordinate of the public key point.</param>
        /// <param name="publicKeyY">The Y coordinate of the public key point.</param>
        /// <param name="signatureR">The R component of the ECDSA signature.</param>
        /// <param name="signatureS">The S component of the ECDSA signature.</param>
        /// <returns><c>true</c> if the signature is valid for the provided message and public key; otherwise, <c>false</c>.</returns>
        public bool VerifySignature(string message, BigInteger publicKeyX, BigInteger publicKeyY, BigInteger signatureR, BigInteger signatureS)
        {
            // Construct the public key and signature points using the provided coordinates
            ECPoint publicKey = new ECPoint(publicKeyX, publicKeyY);
            ECPoint signature = new ECPoint(signatureR, signatureS);

            // Call the signature verification method on the underlying verifier
            return _verifier.VerifySignature(publicKey, signature, message);
        }
    }
}
