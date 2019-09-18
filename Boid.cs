using System;

namespace Boids
{
    internal class Boid
    {
        public Vector Position { get; set; }
        public Vector Velocity { get; set; }

        public BoidColour Colour { get; set; }

        public Boid(Random rnd)
        {
            Position = new Vector
            (
                x: rnd.Next(0, 100),
                y: rnd.Next(0, 100)
            );

            Velocity = new Vector
            (
                x: rnd.Next(0, 10),
                y: rnd.Next(0, 10)
            );

            Colour = (BoidColour)rnd.Next(0, Enum.GetNames(typeof(BoidColour)).Length);
        }
    }

    public enum BoidColour
    {
        Red,
        Blue,
        Yellow,
        Green,
        Purple
    }

}
