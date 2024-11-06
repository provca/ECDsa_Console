using LibECDsa.Operations;
using LibServices.Security.Cryptography.ECDsaService;
using LibServices.Security.Cryptography.ECDsaService.Messages;
using System.Numerics;

namespace LibServices.Security.Cryptography.Factories.Messages
{
    /// <summary>
    /// Factory class for creating and executing the ECDSA signing process.
    /// </summary>
    public class Factory_ECDsaSigner
    {
        /// <summary>
        /// Creates an ECDSA signer and signs the specified message using the provided private key and elliptic curve.
        /// </summary>
        /// <param name="curveName">The name of the elliptic curve to be used for signing.</param>
        /// <param name="privateKey">The private key used to sign the message.</param>
        /// <param name="message">The message to be signed.</param>
        /// <returns>A tuple containing the generated R and s values as <see cref="BigInteger"/>s for the signature.</returns>
        /// <exception cref="ArgumentException">Thrown if the specified curve name is invalid or unsupported.</exception>
        public static (BigInteger RHex, BigInteger sHex) CreateSigner(string curveName, BigInteger privateKey, string message)
        {
            // Initialize curve parameters using the provided curve name
            var curveParameters = new Service_CurveParameters(curveName).Create();

            // Create an instance of the elliptic curve with the specified parameters
            EllipticCurve curve = new(curveParameters);
            BigInteger n = curveParameters.N;

            // Initialize the signing service with the elliptic curve and order
            Service_ECDsaSigner signerService = new(curve, n);

            // Sign the message and return the signature components
            return signerService.SignMessage(message, privateKey);
        }
    }
}
