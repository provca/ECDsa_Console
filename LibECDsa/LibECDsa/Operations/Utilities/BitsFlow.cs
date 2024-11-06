namespace LibECDsa.Operations.Utilities
{
    /// <summary>
    /// Provides utility methods for handling bit and byte operations.
    /// </summary>
    internal class BitsFlow
    {
        /// <summary>
        /// Converts a hexadecimal string to a byte array, ensuring the first byte is not in two's complement form.
        /// </summary>
        /// <param name="hex">The hexadecimal string to convert.</param>
        /// <returns>A byte array representing the hexadecimal string.</returns>
        internal static byte[] GetHexBytes(string hex)
        {
            // Ensure even length by prepending a '0' if necessary
            if (hex.Length % 2 != 0)
                hex = "0" + hex;

            // Create the byte array
            byte[] bytes = new byte[hex.Length / 2];

            // Convert each pair of hexadecimal digits to a byte
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }

            // Ensure the first byte is not interpreted as negative
            if (bytes.Length > 0 && bytes[0] >= 0x80) // if the first byte is >= 0x80
            {
                // Add a leading zero byte to avoid negative interpretation
                byte[] newBytes = new byte[bytes.Length + 1];
                newBytes[0] = 0; // Add zero at the start
                Array.Copy(bytes, 0, newBytes, 1, bytes.Length);
                return newBytes;
            }

            return bytes;
        }
    }
}

