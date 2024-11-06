using System.Numerics;

namespace LibECDsa
{
    /// <summary>
    /// Represents a point on an elliptic curve, defined by its X and Y coordinates.
    /// </summary>
    public class ECPoint
    {
        /// <summary>
        /// Gets or sets the X coordinate of the point on the elliptic curve.
        /// </summary>
        public BigInteger X { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate of the point on the elliptic curve.
        /// </summary>
        public BigInteger Y { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ECPoint"/> class with specified X and Y coordinates.
        /// </summary>
        /// <param name="x">The X coordinate of the elliptic curve point.</param>
        /// <param name="y">The Y coordinate of the elliptic curve point.</param>
        public ECPoint(BigInteger x, BigInteger y)
        {
            // Set X and Y coordinates for this ECPoint
            X = x;
            Y = y;
        }
    }
}

