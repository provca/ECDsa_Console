using LibECDsa.Curves;
using LibECDsa.Enums;
using LibECDsa.Interfaces;

namespace LibServices.Security.Cryptography.ECDsaService
{
    /// <summary>
    /// Provides functionality to create curve parameter instances based on the specified curve name.
    /// </summary>
    public class Service_CurveParameters
    {
        // Name of the elliptic curve to initialize.
        private readonly string _curveName;

        /// <summary>
        /// Initializes a new instance of the <see cref="Service_CurveParameters"/> class with the specified curve name.
        /// </summary>
        /// <param name="curveName">The name of the elliptic curve to use for parameter creation.</param>
        public Service_CurveParameters(string curveName)
        {
            _curveName = curveName;
        }

        /// <summary>
        /// Creates an instance of <see cref="ICurveParameters"/> based on the provided curve name.
        /// </summary>
        /// <returns>An instance of <see cref="ICurveParameters"/> corresponding to the specified curve.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the curve name is null or empty.</exception>
        /// <exception cref="ArgumentException">Thrown if the specified curve cannot be initialized.</exception>
        public ICurveParameters Create()
        {
            // Ensure the curve name is not null or empty.
            if (string.IsNullOrEmpty(_curveName))
            {
                throw new ArgumentNullException(nameof(_curveName), "A name for elliptic curve is required.");
            }

            // Match the curve name to create the appropriate curve parameters.
            return _curveName switch
            {
                nameof(CurveType.secp256k1) => new SecP256K1Parameters(),
                _ => throw new ArgumentException("The elliptic curve could not be initialized.")
            };
        }
    }
}

