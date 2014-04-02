using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ore.Math3D
{
    /// <summary>
    /// Represents a quaternion.
    /// </summary>
    public class Quaternion
    {
        /// <summary>
        /// The X component.
        /// </summary>
        public double X;
        /// <summary>
        /// The Y component.
        /// </summary>
        public double Y;
        /// <summary>
        /// The Z component.
        /// </summary>
        public double Z;
        /// <summary>
        /// The W component.
        /// </summary>
        public double W;

        /// <summary>
        /// Create a new quaternion from the specified components.
        /// </summary>
        /// <param name="X">The X component.</param>
        /// <param name="Y">The Y component.</param>
        /// <param name="Z">The Z component.</param>
        /// <param name="W">The W component.</param>
        public Quaternion(double X, double Y, double Z, double W)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.W = W;
        }

        /// <summary>
        /// Get the Matrix4 representation of this quaternion
        /// </summary>
        public Matrix4 Matrix
        {
            get
            {
                Matrix4 m = Matrix4.Identity;
                m[0] = 1 - 2 * (this.Y * this.Y + this.Z * this.Z);
                m[1] = 2 * (this.X * this.Y + this.Z * this.W);
                m[2] = 2 * (this.X * this.Z - this.Y * this.W);
                m[3] = 0;
                m[4] = 2 * (this.X * this.Y - this.Z * this.W);
                m[5] = 1 - 2 * (this.X * this.X + this.Z * this.Z);
                m[6] = 2 * (this.Z * this.Y + this.X * this.W);
                m[7] = 0;
                m[8] = 2 * (this.X * this.Z + this.Y * this.W);
                m[9] = 2 * (this.Y * this.Z - this.X * this.W);
                m[10] = 1 - 2 * (this.X * this.X + this.Y * this.Y);
                m[11] = 0;
                m[12] = 0;
                m[13] = 0;
                m[14] = 0;
                m[15] = 1;
                return m;
            }
        }

        /// <summary>
        /// Create a new quaternion from the specified rotation.
        /// </summary>
        /// <param name="axis">The axis to rotate around.</param>
        /// <param name="radians">The radians to rotate.</param>
        /// <returns>The Quaternion made from this rotation.</returns>
        public static Quaternion Rotation(Vector3 axis, double radians)
        {
            Quaternion q = Quaternion.Identity;
            double sin = Math.Sin(radians * 0.5);
            q.W = Math.Cos(radians * 0.5);
            q.X = axis.X * sin;
            q.Y = axis.Y * sin;
            q.Z = axis.Z * sin;
            return q;
        }

        public static Quaternion operator *(Quaternion a, Quaternion b)
        {
            Quaternion newQ = Quaternion.Identity;
            newQ.X = a.W * b.X + a.X * b.W + a.Y * b.Z - a.Z * b.Y;
            newQ.Y = a.W * b.Y - a.X * b.Z + a.Y * b.W + a.Z * b.X;
            newQ.Z = a.W * b.Z + a.X * b.Y - a.Y * b.X + a.Z * b.W;
            newQ.W = a.W * b.W - a.X * b.X - a.Y * b.Y - a.Z * b.Z;
            return newQ;
        }

        /// <summary>
        /// Returns a new Identity quaternion.
        /// </summary>
        public static Quaternion Identity
        {
            get { return new Quaternion(0, 0, 0, 1); }
        }
    }
}