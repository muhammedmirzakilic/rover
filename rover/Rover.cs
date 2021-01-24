using System;
namespace rover
{
    public class Rover
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int MaxX { get; private set; }
        public int MaxY { get; private set; }
        public Direction Heading { get; private set; }

        public Rover(int initialX, int initialY, Direction heading, int maxX, int maxY)
        {
            X = initialX;
            Y = initialY;
            Heading = heading;
            MaxX = maxX;
            MaxY = maxY;
        }

        public override string ToString()
        {
            return $"{X} {Y} {Heading}";
        }
    }
}
