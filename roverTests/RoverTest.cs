using System;
using rover;
using Xunit;

namespace roverTests
{
    public class RoverTest
    {
        [Fact]
        public void Construct_CanConstruct()
        {
            var rover = new Rover(0, 0, Direction.N, 0, 0);
            Assert.NotNull(rover);
        }

        [Fact]
        public void Construct_CanSetInitialX()
        {
            var rover = new Rover(1, 0, Direction.N, 0, 0);
            Assert.Equal(1, rover.X);
        }

        [Fact]
        public void Construct_CanSetInitialY()
        {
            var rover = new Rover(0, 1, Direction.N, 0, 0);
            Assert.Equal(1, rover.Y);
        }

        [Fact]
        public void Construct_CanSetInitialHeading()
        {
            var rover = new Rover(0, 1, Direction.E, 0, 0);
            Assert.Equal(Direction.E, rover.Heading);
        }

        [Fact]
        public void Construct_CanSetInitialMaxX()
        {
            var rover = new Rover(1, 0, Direction.N, 1, 0);
            Assert.Equal(1, rover.MaxX);
        }

        [Fact]
        public void Construct_CanSetInitialMaxY()
        {
            var rover = new Rover(0, 0, Direction.N, 0, 1);
            Assert.Equal(1, rover.MaxY);
        }

        [Fact]
        public void RunInstruction_CanChangeHeadingFromEastToNorth()
        {
            var rover = new Rover(0, 0, Direction.E, 1, 1);
            rover.RunInstruction(Instruction.L);
            Assert.Equal(Direction.N, rover.Heading);
        }

        [Fact]
        public void RunInstruction_CanChangeHeadingFromNorthToWest()
        {
            var rover = new Rover(0, 0, Direction.N, 1, 1);
            rover.RunInstruction(Instruction.L);
            Assert.Equal(Direction.W, rover.Heading);
        }

        [Fact]
        public void RunInstruction_CanChangeHeadingFromWestToSouth()
        {
            var rover = new Rover(0, 0, Direction.W, 1, 1);
            rover.RunInstruction(Instruction.L);
            Assert.Equal(Direction.S, rover.Heading);
        }

        [Fact]
        public void RunInstruction_CanChangeHeadingFromSouthToEast()
        {
            var rover = new Rover(0, 0, Direction.S, 1, 1);
            rover.RunInstruction(Instruction.L);
            Assert.Equal(Direction.E, rover.Heading);
        }

        [Fact]
        public void RunInstruction_CanChangeHeadingFromNorthToEast()
        {
            var rover = new Rover(0, 0, Direction.N, 1, 1);
            rover.RunInstruction(Instruction.R);
            Assert.Equal(Direction.E, rover.Heading);
        }

        [Fact]
        public void RunInstruction_CanChangeHeadingFromWestToNorth()
        {
            var rover = new Rover(0, 0, Direction.W, 1, 1);
            rover.RunInstruction(Instruction.R);
            Assert.Equal(Direction.N, rover.Heading);
        }

        [Fact]
        public void RunInstruction_CanChangeHeadingFromSouthToWest()
        {
            var rover = new Rover(0, 0, Direction.S, 1, 1);
            rover.RunInstruction(Instruction.R);
            Assert.Equal(Direction.W, rover.Heading);
        }

        [Fact]
        public void RunInstruction_CanChangeHeadingFromEastToSouth()
        {
            var rover = new Rover(0, 0, Direction.E, 1, 1);
            rover.RunInstruction(Instruction.R);
            Assert.Equal(Direction.S, rover.Heading);
        }

        [Fact]
        public void RunInstruction_CanMoveToEast()
        {
            var rover = new Rover(0, 0, Direction.E, 1, 1);
            rover.RunInstruction(Instruction.M);
            Assert.Equal(1, rover.X);
        }

        [Fact]
        public void RunInstruction_CanMoveToNorth()
        {
            var rover = new Rover(0, 0, Direction.N, 1, 1);
            rover.RunInstruction(Instruction.M);
            Assert.Equal(1, rover.Y);
        }

        [Fact]
        public void RunInstruction_CanMoveToWest()
        {
            var rover = new Rover(1, 1, Direction.W, 1, 1);
            rover.RunInstruction(Instruction.M);
            Assert.Equal(0, rover.X);
        }

        [Fact]
        public void RunInstruction_CanMoveToSouth()
        {
            var rover = new Rover(1, 1, Direction.S, 1, 1);
            rover.RunInstruction(Instruction.M);
            Assert.Equal(0, rover.Y);
        }

        [Fact]
        public void RunInstruction_ShouldNotMove_IfItIsInEastCorner()
        {
            var rover = new Rover(1, 1, Direction.E, 1, 1);
            rover.RunInstruction(Instruction.M);
            Assert.Equal(1, rover.X);
        }

        [Fact]
        public void RunInstruction_ShouldNotMove_IfItIsInNorthCorner()
        {
            var rover = new Rover(1, 1, Direction.N, 1, 1);
            rover.RunInstruction(Instruction.M);
            Assert.Equal(1, rover.Y);
        }

        [Fact]
        public void RunInstruction_ShouldNotMove_IfItIsInWestCorner()
        {
            var rover = new Rover(0, 0, Direction.W, 1, 1);
            rover.RunInstruction(Instruction.M);
            Assert.Equal(0, rover.X);
        }

        [Fact]
        public void RunInstruction_ShouldNotMove_IfItIsInSouthCorner()
        {
            var rover = new Rover(0, 0, Direction.S, 1, 1);
            rover.RunInstruction(Instruction.M);
            Assert.Equal(0, rover.Y);
        }
    }
}
