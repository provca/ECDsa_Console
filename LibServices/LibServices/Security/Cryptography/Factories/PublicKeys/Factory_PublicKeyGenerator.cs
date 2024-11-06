using LibServices.Security.Cryptography.ECDsaService.PublicKeys;
using System.Numerics;

namespace LibServices.Security.Cryptography.Factories.PublicKeys
{
    /// <summary>
    /// Factory class for creating instances of <see cref="Service_GeneratePublicKeysFromCurve"/>.
    /// </summary>
    public class Factory_PublicKeyGenerator
    {
        /// <summary>
        /// Creates a new instance of the <see cref="Service_GeneratePublicKeysFromCurve"/> class
        /// with the specified elliptic curve name and private key.
        /// </summary>
        /// <param name="curveName">The name of the elliptic curve to use.</param>
        /// <param name="privateKey">The private key for which to generate the public key.</param>
        /// <returns>A new instance of <see cref="Service_GeneratePublicKeysFromCurve"/>.</returns>
        public static Service_GeneratePublicKeysFromCurve Create(string curveName, BigInteger privateKey)
        {
            // Return a new instance of Service_GeneratePublicKeysFromCurve with provided parameters.
            return new Service_GeneratePublicKeysFromCurve(curveName, privateKey);
        }
    }
}
