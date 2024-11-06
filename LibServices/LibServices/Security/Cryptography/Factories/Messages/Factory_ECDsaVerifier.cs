using LibECDsa.Operations;
using LibServices.Security.Cryptography.ECDsaService;
using LibServices.Security.Cryptography.ECDsaService.Messages;

namespace LibServices.Security.Cryptography.Factories.Messages
{
    /// <summary>
    /// Factory class for creating instances of the <see cref="Service_ECDsaVerifier"/> class.
    /// </summary>
    public static class Factory_ECDsaVerifier
    {
        /// <summary>
        /// Creates an instance of <see cref="Service_ECDsaVerifier"/> configured with the specified elliptic curve.
        /// </summary>
        /// <param name="curveName">The name of the elliptic curve to be used for verification.</param>
        /// <returns>A new instance of <see cref="Service_ECDsaVerifier"/>.</returns>
        /// <exception cref="ArgumentException">Thrown if the specified curve name is invalid or unsupported.</exception>
        public static Service_ECDsaVerifier CreateVerifier(string curveName)
        {
            // Initialize curve parameters using the provided curve name
            var curveParameters = new Service_CurveParameters(curveName).Create();

            // Create an instance of the elliptic curve with the specified parameters
            EllipticCurve curve = new EllipticCurve(curveParameters);

            // Return a configured ECDSA verifier service
            return new Service_ECDsaVerifier(curveParameters, curve);
        }
    }
}

