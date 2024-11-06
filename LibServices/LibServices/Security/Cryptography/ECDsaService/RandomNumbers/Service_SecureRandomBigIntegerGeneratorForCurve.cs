using LibECDsa.Interfaces;
using LibECDsa.Operations;
using LibECDsa.Operations.Utilities;
using LibECDsa.RandomNumbers;
using System.Numerics;

namespace LibServices.Security.Cryptography.ECDsaService.RandomNumbers
{
    /// <summary>
    /// Generates a secure random BigInteger (private key) for a specified elliptic curve.
    /// </summary>
    public class Service_SecureRandomBigIntegerGeneratorForCurve
    {
        // The name of the elliptic curve.
        private readonly string _curveName;

        // The generated private key.
        private BigInteger _privateKey;

        /// <summary>
        /// Gets the hexadecimal representation of the private key.
        /// </summary>
        public string PrivateKeyHex => NumeralSystem.HexaDecimal(_privateKey);

        /// <summary>
        /// Gets the decimal representation of the private key.
        /// </summary>
        public string PrivateKeyDecimal => NumeralSystem.Decimal(_privateKey);

        /// <summary>
        /// Gets the octal representation of the private key.
        /// </summary>
        public string PrivateKeyOctal => NumeralSystem.Octal(_privateKey);

        /// <summary>
        /// Gets the Base64 representation of the private key.
        /// </summary>
        public string PrivateKeyBase64 => NumeralSystem.ToBase64(_privateKey);

        /// <summary>
        /// Gets the PEM format of the private key.
        /// </summary>
        public string PrivateKeyPEM => NumeralSystem.PEM(_privateKey);

        /// <summary>
        /// Gets the DER format of the private key in Base64 encoding.
        /// </summary>
        public string PrivateKeyDER => NumeralSystem.DerBase64(_privateKey);

        /// <summary>
        /// Gets the private key as a <see cref="BigInteger"/>.
        /// </summary>
        public BigInteger PrivateKey => _privateKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="Service_SecureRandomBigIntegerGeneratorForCurve"/> class
        /// with the specified elliptic curve name and generates a random private key.
        /// </summary>
        /// <param name="curveName">The name of the elliptic curve.</param>
        public Service_SecureRandomBigIntegerGeneratorForCurve(string curveName)
        {
            _curveName = curveName; // Store the curve name.
            _privateKey = GenerateRandomBigIntegerForCurve(); // Generate the private key upon initialization.
        }

        /// <summary>
        /// Generates a secure random BigInteger (private key) constrained by the specified elliptic curve parameters.
        /// </summary>
        /// <returns>A randomly generated private key as a <see cref="BigInteger"/>.</returns>
        private BigInteger GenerateRandomBigIntegerForCurve()
        {
            // Create curve parameters based on the curve name.
            ICurveParameters curveParameters = new Service_CurveParameters(_curveName).Create();

            // Initialize the elliptic curve with the specified parameters.
            EllipticCurve curve = new(curveParameters);

            // Create a secure random generator for BigIntegers based on the elliptic curve.
            SecureRandomBigIntegerGenerator secureRandomBigIntegerGenerator = new(curve);

            // Generate and return a random private key that is less than the curve's order.
            return _privateKey = secureRandomBigIntegerGenerator.GenerateRandomBigIntegerForCurve(curveParameters.N);
        }
    }
}

