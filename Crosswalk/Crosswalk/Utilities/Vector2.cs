using System;
using System.Drawing;

namespace Crosswalk
{
    public class Vector2
    {
        private static readonly Vector2 _Zero = new Vector2(0, 0);
        
        public float X { get; set; }

        public float Y { get; set; }
        
        public float Length
        {
            get
            {
                return (float)Math.Sqrt(X * X + Y * Y);
            }
        }

        /// <summary>
        /// A vector with values X = 0 and Y = 0
        /// </summary>
        public static Vector2 Zero
        {
            get
            {
                return _Zero;
            }
        }

        /// <summary>
        /// Initialises a vector with the given values.
        /// </summary>
        /// <param name="X">The X value.</param>
        /// <param name="Y">The Y value.</param>
        public Vector2(float X, float Y)
        {
            this.X = X;
            this.Y = Y;
        }

        /// <summary>
        /// Initialises a vector with X = Value and Y = Value
        /// </summary>
        /// <param name="Value">The value for X and Y.</param>
        public Vector2(float Value)
        {
            this.X = Value;
            this.Y = Value;
        }
        
        /// <summary>
        /// Normalisiert diesen Vektor.
        /// </summary>
        public void Normalize()
        {
            float Len = this.Length;
            this.X /= Len;
            this.Y /= Len;
        }

        /// <summary>
        /// Rotate the vector around a given origin.
        /// </summary>
        /// <param name="Origin">The origin of the rotation.</param>
        /// <param name="Angle">Angle in radians.</param>
        public void RotateAround(Vector2 Origin, float Angle)
        {
            float cos = (float)Math.Cos(Angle);
            float sin = (float)Math.Sin(Angle);
            Vector2 Translated = this - Origin;
            X = cos * Translated.X + sin * Translated.Y;
            Y = sin * Translated.X + cos * Translated.Y;
            X += Origin.X;
            Y += Origin.Y;
        }
        
        /// <summary>
        /// Returns the normalised vector
        /// (Does not alter this vector instance)
        /// </summary>
        /// <param name="Vector">The vector to be normalised.</param>
        /// <returns></returns>
        public static Vector2 Normalize(Vector2 Vector)
        {
            return new Vector2(Vector.X / Vector.Length, Vector.Y / Vector.Length);
        }

        /// <summary>
        /// Returns the distance between to vectors.
        /// </summary>
        /// <param name="A">The first vector.</param>
        /// <param name="B">The second vector.</param>
        /// <returns></returns>
        public static float Distance(Vector2 A, Vector2 B)
        {
            return (float)Math.Sqrt(Math.Pow(B.X - A.X, 2) + Math.Pow(B.Y - A.Y, 2));
        }

        /// <summary>
        /// Returns the angle (in radians) between the two vectors.
        /// </summary>
        /// <param name="A">The first vector.</param>
        /// <param name="B">The second vector.</param>
        /// <returns></returns>
        public static float Angle(Vector2 A, Vector2 B)
        {
            return (float)Math.Atan2(B.Y - A.Y, B.X - A.X);
        }

        /// <summary>
        /// Returns the angle (in radians) of a vector.
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        public static float Angle(Vector2 A)
        {
            return (float)Math.Atan2(A.Y, A.X);
        }

        /// <summary>
        /// Creates and returns a new vector with the given angle and length.
        /// </summary>
        /// <param name="Angle">The angle in radians.</param>
        /// <patam name="Length">The length of the vector.</patam>
        /// <returns></returns>
        public static Vector2 FromAngle(float Angle, float Length = 1f)
        {
            return new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle)) * Length;
        }
        
        public static Vector2 operator+(Vector2 First, Vector2 Second)
        {
            return new Vector2(First.X + Second.X, First.Y + Second.Y);
        }

        public static Vector2 operator -(Vector2 First, Vector2 Second)
        {
            return new Vector2(First.X - Second.X, First.Y - Second.Y);
        }

        public static Vector2 operator *(Vector2 Vector, float Scalar)
        {
            return new Vector2(Vector.X * Scalar, Vector.Y * Scalar);
        }
        
        public PointF ToPointF()
        {
            return new PointF(X, Y);
        }
        
        public Point ToPoint()
        {
            return new Point((int)X, (int)Y);
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", X, Y);
        }   
    }
}
