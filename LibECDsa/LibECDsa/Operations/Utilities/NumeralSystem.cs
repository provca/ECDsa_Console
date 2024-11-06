using System.Formats.Asn1;
using System.Numerics;
using System.Text;

namespace LibECDsa.Operations.Utilities
{
    public class NumeralSystem
    {
        /// <summary>
        /// Base64 representation without delimiters PEM.
        /// </summary>
        public static string ToBase64(BigInteger value) => Convert.ToBase64String(value.ToByteArray());

        /// <summary>
        /// Base64 representation without delimiters PEM.
        /// </summary>
        public static string ToBase64(byte[] value) => Convert.ToBase64String(value);

        /// <summary>
        /// PEM representation of the BigInteger value (private key).
        /// </summary>
        public static string PEM(BigInteger value)
        {
            string base64Key = ToBase64(value.ToByteArray());
            return FormatPem("-----BEGIN PRIVATE KEY-----", "-----END PRIVATE KEY-----", base64Key);
        }

        /// <summary>
        /// PEM representation of the byte array (public key).
        /// </summary>
        public static string PEM(byte[] value)
        {
            string base64Key = ToBase64(value);
            return FormatPem("-----BEGIN PUBLIC KEY-----", "-----END PUBLIC KEY-----", base64Key);
        }

        /// <summary>
        /// Base64 representation in DER format.
        /// </summary>
        public static string DerBase64(BigInteger value) => Convert.ToBase64String(DER(value));

        /// <summary>
        /// Base64 representation in DER format.
        /// </summary>
        public static string DerBase64(BigInteger x, BigInteger y) => Convert.ToBase64String(DER(x, y));

        /// <summary>
        /// Hexadecimal representation without prefix and leading zeros.
        /// </summary>
        public static string HexaDecimal(BigInteger value) => RemoveLeadingZero(value.ToString("X"));

        /// <summary>
        /// Octal representation without prefix and leading zeros.
        /// </summary>
        public static string Octal(BigInteger value) => RemoveLeadingZero(ConvertBigIntegerToOctal(value));

        /// <summary>
        /// Decimal representation without prefix and leading zeros.
        /// </summary>
        public static string Decimal(BigInteger value) => RemoveLeadingZero(value.ToString());

        /// <summary>
        /// Converts a BigInteger value to its octal string representation.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns></returns>
        public static string ConvertBigIntegerToOctal(BigInteger value)
        {
            List<string> octalDigits = new List<string>();

            // Base for octal representation.
            BigInteger eight = 8;

            while (value > 0)
            {
                // Append the last octal digit.
                octalDigits.Add((value % eight).ToString());

                // Divide by 8 to move to the next digit.
                value /= eight;
            }

            // Reverse to get the correct order.
            octalDigits.Reverse();

            // Join digits to form the octal string.
            return string.Join("", octalDigits);
        }

        /// <summary>
        /// Removes leading zeros from a string representation of a number
        /// </summary>
        /// <param name="value">Value convert to string to remove zeros.</param>
        /// <returns></returns>
        public static string RemoveLeadingZero(string value)
        {
            // Trim leading zero if present.
            return value.StartsWith("0") ? value.TrimStart('0') : value;
        }

        /// <summary>
        /// Method to generate the byte array in DER format.
        /// </summary>
        /// <returns>A byte array of the value.</returns>
        private static byte[] DER(BigInteger value)
        {
            var writer = new AsnWriter(AsnEncodingRules.DER);

            // Main ASN.1 sequence.
            writer.PushSequence();

            // Add private key or point X in ASN.1 INTEGER.
            writer.WriteInteger(value);

            // End main sequence.
            writer.PopSequence();

            return writer.Encode();
        }

        /// <summary>
        /// Method to generate the byte array in DER format for ECPoints.
        /// </summary>
        /// <returns>A byte array of the two values.</returns>
        private static byte[] DER(BigInteger x, BigInteger y)
        {
            var writer = new AsnWriter(AsnEncodingRules.DER);

            // Main ASN.1 sequence.
            writer.PushSequence();

            // Add point X in ASN.1 INTEGER.
            writer.WriteInteger(x);

            // Add point Y in ASN.1 INTEGER.
            writer.WriteInteger(y);

            // End main sequence.
            writer.PopSequence();

            return writer.Encode();
        }

        /// <summary>
        /// Generates the PEM representation for a given base64-encoded value.
        /// </summary>
        private static string FormatPem(string header, string footer, string base64Key)
        {
            StringBuilder pemBuilder = new StringBuilder($"{header}\n");

            int offset = 0;
            while (offset < base64Key.Length)
            {
                int lineLength = Math.Min(64, base64Key.Length - offset);
                pemBuilder.AppendLine(base64Key.Substring(offset, lineLength));
                offset += lineLength;
            }

            pemBuilder.AppendLine($"{footer}");
            return pemBuilder.ToString();
        }
    }
}
