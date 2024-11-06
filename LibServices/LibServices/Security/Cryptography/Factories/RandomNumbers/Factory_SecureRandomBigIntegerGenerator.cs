using LibServices.Security.Cryptography.ECDsaService.RandomNumbers;

namespace LibServices.Security.Cryptography.Factories.RandomNumbers
{
    /// <summary>
    /// Factory class for creating instances of <see cref="Service_SecureRandomBigIntegerGeneratorForCurve"/>.
    /// </summary>
    public class Factory_SecureRandomBigIntegerGenerator
    {
        /// <summary>
        /// Creates a new instance of the <see cref="Service_SecureRandomBigIntegerGeneratorForCurve"/> class
        /// with the specified elliptic curve name.
        /// </summary>
        /// <param name="curveName">The name of the elliptic curve to use for random number generation.</param>
        /// <returns>A new instance of <see cref="Service_SecureRandomBigIntegerGeneratorForCurve"/>.</returns>
        public static Service_SecureRandomBigIntegerGeneratorForCurve Create(string curveName)
        {
            // Return a new instance of Service_SecureRandomBigIntegerGeneratorForCurve with the provided curve name.
            return new Service_SecureRandomBigIntegerGeneratorForCurve(curveName);
        }
    }
}
