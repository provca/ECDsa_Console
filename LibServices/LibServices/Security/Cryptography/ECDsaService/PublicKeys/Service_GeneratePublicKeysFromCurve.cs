using LibECDsa.Interfaces;
using LibECDsa.Operations;
using LibECDsa.Operations.PublicKeys;
using System.Numerics;

namespace LibServices.Security.Cryptography.ECDsaService.PublicKeys
{
    /// <summary>
    /// Provides functionality to generate various representations of public keys from a specified elliptic curve and private key.
    /// </summary>
    public class Service_GeneratePublicKeysFromCurve
    {
        private readonly string _curveName; // The name of the elliptic curve.
        private readonly BigInteger _privateKey; // The private key for generating the public key.

        /// <summary>
        /// Gets the hexadecimal representation of the public key.
        /// </summary>
        public string PublicKeyHex => GeneratePublicKeysFromCurve().Hexadecimal;

        /// <summary>
        /// Gets the decimal representation of the public key.
        /// </summary>
        public string PublicKeyDecimal => GeneratePublicKeysFromCurve().Decimal;

        /// <summary>
        /// Gets the octal representation of the public key.
        /// </summary>
        public string PublicKeyOctal => GeneratePublicKeysFromCurve().Octal;

        /// <summary>
        /// Gets the Base64 representation of the public key.
        /// </summary>
        public string PublicKeyBase64 => GeneratePublicKeysFromCurve().PublicKeyBase64;

        /// <summary>
        /// Gets the PEM format of the public key in Base64 encoding.
        /// </summary>
        public string PublicKeyPEM => GeneratePublicKeysFromCurve().PublicKeyPemBase64;

        /// <summary>
        /// Gets the DER format of the public key in Base64 encoding.
        /// </summary>
        public string PublicKeyDER => GeneratePublicKeysFromCurve().PublicKeyDerBase64;

        /// <summary>
        /// Gets the compressed byte array representation of the public key.
        /// </summary>
        public byte[] PublicKeyCompressed => GeneratePublicKeysFromCurve().Compressed;

        /// <summary>
        /// Gets the uncompressed byte array representation of the public key.
        /// </summary>
        public byte[] PublicKeyUncompressed => GeneratePublicKeysFromCurve().Uncompressed;

        /// <summary>
        /// Gets the hexadecimal representation of the compressed public key.
        /// </summary>
        public string PublicKeyCompressedHex => BitConverter.ToString(PublicKeyCompressed).Replace("-", "");

        /// <summary>
        /// Gets the hexadecimal representation of the uncompressed public key.
        /// </summary>
        public string PublicKeyUncompressedHex => BitConverter.ToString(PublicKeyUncompressed).Replace("-", "");

        /// <summary>
        /// Gets the X-coordinate of the public key point.
        /// </summary>
        public BigInteger PublicKey_X => GeneratePublicKeysFromCurve().X;

        /// <summary>
        /// Gets the Y-coordinate of the public key point.
        /// </summary>
        public BigInteger PublicKey_Y => GeneratePublicKeysFromCurve().Y;

        /// <summary>
        /// Initializes a new instance of the <see cref="Service_GeneratePublicKeysFromCurve"/> class with the specified curve name and private key.
        /// </summary>
        /// <param name="curveName">The name of the elliptic curve.</param>
        /// <param name="privateKey">The private key used to generate the public key.</param>
        public Service_GeneratePublicKeysFromCurve(string curveName, BigInteger privateKey)
        {
            _curveName = curveName;
            _privateKey = privateKey;
        }

        /// <summary>
        /// Generates the public key representations based on the specified elliptic curve and private key.
        /// </summary>
        /// <returns>A <see cref="PublicKeyRepresentation"/> object containing different formats of the public key.</returns>
        private PublicKeyRepresentation GeneratePublicKeysFromCurve()
        {
            // Create curve parameters based on the curve name.
            ICurveParameters curveParameters = new Service_CurveParameters(_curveName).Create();

            // Initialize the elliptic curve with the specified parameters.
            EllipticCurve curve = new(curveParameters);

            // Create a serializer to generate public key representations.
            PublicKeySerializer serializer = new(curve);

            // Generate and return the public key representations.
            return serializer.GetPublicKeyRepresentations(_privateKey);
        }
    }
}

