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

        public void RunInstruction(Instruction instruction)
        {
            if (instruction == Instruction.L) ChangeHeading(1);
            else if (instruction == Instruction.R) ChangeHeading(-1);
            else if (instruction == Instruction.M) Move();
            else throw new Exception("Unrecognized instruction!");
        }

        private void Move()
        {
            if (Heading == Direction.E) X++;
            else if (Heading == Direction.N) Y++;
            else if (Heading == Direction.W) X--;
            else if (Heading == Direction.S) Y--;

            //prevent rover from getting out of range
            if (X < 0) X = 0;
            if (X > MaxX) X = MaxX;
            if (Y < 0) Y = 0;
            if (Y > MaxY) Y = MaxY;
        }

        private void ChangeHeading(int rotateTo)
        {
            Heading = (Direction)((int)(Heading + (byte)rotateTo) % 4);
        }

        public override string ToString()
        {
            return $"{X} {Y} {Heading}";
        }
    }
}
