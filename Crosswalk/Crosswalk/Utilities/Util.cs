using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Crosswalk
{
    public static class Util
    {
        public static Random Rand = new Random();
        
        private static readonly float degtorad = (float)(180.0 / Math.PI);
        private static readonly float radtodeg = (float)(Math.PI / 180.0);
        
        public static float DEG_TO_RAD { get { return degtorad; } }
        public static float RAD_TO_DEG { get { return radtodeg; } }
        
        public static float ToDegrees(float Radians)
        {
            return DEG_TO_RAD * Radians;
        }
        
        public static float ToRadians(float Degrees)
        {
            return RAD_TO_DEG * Degrees;
        }

        /// <summary>
        /// Returns true "Percentage" of the time, otherwise false.
        /// </summary>
        /// <param name="Percentage"></param>
        /// <returns></returns>
        public static bool Chance(float Percentage)
        {
            return Rand.NextDouble() < Percentage;
        }

        /// <summary>
        /// Returns a random color.
        /// </summary>
        /// <returns></returns>
        public static Color RandomColor(int Alpha = 255)
        {
            return Color.FromArgb(Alpha, Rand.Next(256), Rand.Next(256), Rand.Next(256));
        }

        /// <summary>
        /// Returns a normalised vector with a random rotation.
        /// </summary>
        /// <returns></returns>
        public static Vector2 RandomDirection()
        {
            return Vector2.FromAngle(Util.RandomAngle(true));
        }

        /// <summary>
        /// Returns a random float between 0 and Max.
        /// </summary>
        /// <param name="Max">The maximum.</param>
        /// <returns></returns>
        public static float Float(float Max = 1f)
        {
            return (float)Rand.NextDouble() * Max;
        }

        public static float RandFloat(float Min, float Max)
        {
            return ((float)Rand.NextDouble()) * (Max - Min) + Min;
        }

        /// <summary>
        /// Returns a random angle.
        /// </summary>
        /// <param name="Radians">If true, angle is in radians.</param>
        /// <returns></returns>
        public static float RandomAngle(bool Radians = false)
        {
            float Angle = Rand.Next(361);
            if (Radians)
                return Util.ToRadians(Angle);

            return Angle;
        }

        /// <summary>
        /// Returns an enclosing rectangle for a given circle.
        /// Useful for drawing with GDI+.
        /// </summary>
        /// <param name="Position"></param>
        /// <param name="Radius"></param>
        /// <returns></returns>
        public static RectangleF ToEllipseRect(Vector2 Position, float Radius)
        {
            return new RectangleF(
                Position.X - Radius,
                Position.Y - Radius,
                Radius * 2,
                Radius * 2
                );
        }

        /// <summary>
        /// Flips an angle (in degrees)
        /// </summary>
        /// <param name="Angle">Der Winkel (in Grad).</param>
        /// <returns></returns>
        public static float InvertAngle(float Angle)
        {
            return (Angle + 180) % 360;
        }

        /// <summary>
        /// Lerps between two values.
        /// </summary>
        /// <param name="Percent">Progress in percent.</param>
        /// <returns></returns>
        public static float Lerp(float From, float To, float Percent)
        {
            return (From + Percent * (To - From));
        }

        /// <summary>
        /// Allows a float to approach a given value, speed limited by MaxStep.
        /// </summary>
        /// <param name="MaxStep">Maximum change per step.</param>
        /// <returns></returns>
        public static float Approach(float From, float To, float MaxStep)
        {
            return From > To ? Math.Max(From - MaxStep, To) : Math.Min(From + MaxStep, To);
        }
        
        /// <summary>
        /// Returns a random element from a list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetRandom<T>(this List<T> TheList)
        {
            return TheList[Rand.Next(TheList.Count)];
        }

        /// <summary>
        /// Returns a random value form an enum.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public static TEnum RandomEnumValue<TEnum>() where TEnum : struct,  IComparable, IFormattable, IConvertible
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToList().GetRandom();
        }

        public static float PowF(float Base, float Exponent)
        {
            return (float)Math.Pow((double)Base, (double)Exponent);
        }
    }
}
