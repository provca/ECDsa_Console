using LibECDsa;
using LibECDsa.Operations;
using LibECDsa.Operations.Messages;
using System.Numerics;

namespace LibServices.Security.Cryptography.ECDsaService.Messages
{
    /// <summary>
    /// Provides services for generating ECDSA signatures.
    /// </summary>
    public class Service_ECDsaSigner
    {
        private readonly EllipticCurve _curve;
        private readonly BigInteger _n;

        /// <summary>
        /// Initializes a new instance of the <see cref="Service_ECDsaSigner"/> class with the specified elliptic curve and order.
        /// </summary>
        /// <param name="curve">The elliptic curve to use for signing.</param>
        /// <param name="n">The order of the base point on the elliptic curve.</param>
        public Service_ECDsaSigner(EllipticCurve curve, BigInteger n)
        {
            _curve = curve;
            _n = n;
        }

        /// <summary>
        /// Signs a message using the ECDSA algorithm and a private key.
        /// </summary>
        /// <param name="message">The message to be signed.</param>
        /// <param name="privateKey">The private key used to generate the signature.</param>
        /// <returns>A tuple containing the R and S components of the ECDSA signature.</returns>
        public (BigInteger R, BigInteger s) SignMessage(string message, BigInteger privateKey)
        {
            // Create an instance of the ECDSA signer with the specified curve and order
            ECDsaSigner signer = new(_curve, _n);

            // Generate the ECDSA signature for the message
            ECPoint signature = signer.SignMessage(message, privateKey);

            // Extract the R and S components from the signature point
            BigInteger R = signature.X;
            BigInteger s = signature.Y;

            return (R, s);
        }
    }
}

