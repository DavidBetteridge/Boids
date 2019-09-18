using System.Collections.Generic;
using System.Linq;

namespace Boids
{
    internal struct Vector
    {
        public int X { get; private set; }

        public int Y { get; private set; }

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Vector operator +(Vector a, Vector b) => new Vector(a.X + b.X, a.Y + b.Y);

        public static Vector operator -(Vector a, Vector b) => new Vector(a.X - b.X, a.Y - b.Y);

        public static Vector operator /(Vector a, int b) => new Vector(a.X / b, a.Y / b);

        public static Vector Zero => new Vector(0, 0);

        internal Vector WithX(int newX) => new Vector(newX, Y);

        internal Vector WithY(int newY) => new Vector(X, newY);
    }

    internal static class VectorExtensions
    {
        public static Vector Sum(this IEnumerable<Vector> vectors) => vectors.Aggregate((v1, v2) => v1 + v2);

        public static Vector Average(this IEnumerable<Vector> vectors) => vectors.Sum() / vectors.Count();
    }
}
