using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using rover;
using Xunit;

namespace roverTests
{
    public class NasaTest
    {
        [Fact]
        public void Run_ShouldThrowError_IfRandomFileGiven()
        {
            var fileSystem = new MockFileSystem();
            var filePath = "randomfilePath";

            var nasa = new Nasa(fileSystem);

            var exception = Assert.Throws<Exception>(() => nasa.Run(filePath));
            Assert.Equal("Instruction file not found!", exception.Message);
        }

        [Fact]
        public void Run_ShouldThrowError_IfInstructionSetIsInvalid()
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
                {
                    { @"./randomfilePath.txt", new MockFileData("") },
                });
            var filePath = "./randomfilePath.txt";

            var nasa = new Nasa(fileSystem);
            var exception = Assert.Throws<Exception>(() => nasa.Run(filePath));

            Assert.Equal("Invalid instruction set!", exception.Message);
        }

        [Fact]
        public void Run_ShouldThrowError_IfPlateauCoordinatesIsInvalid()
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
                {
                    { @"./randomfilePath.txt", new MockFileData(
                        "A B" + Environment.NewLine +
                        "1 1 N" + Environment.NewLine +
                        "LRMMMM") },
                });
            var filePath = "./randomfilePath.txt";

            var nasa = new Nasa(fileSystem);
            var exception = Assert.Throws<Exception>(() => nasa.Run(filePath));

            Assert.Equal("Invalid plateau coordinates!", exception.Message);
        }

        [Fact]
        public void Run_ShouldThrowError_IfRoverPositionIsInvalid()
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
                {
                    { @"./randomfilePath.txt", new MockFileData(
                        "5 5" + Environment.NewLine +
                        "A 1 N" + Environment.NewLine +
                        "LRMMMM") },
                });
            var filePath = "./randomfilePath.txt";

            var nasa = new Nasa(fileSystem);
            var exception = Assert.Throws<Exception>(() => nasa.Run(filePath));

            Assert.Equal("Invalid rover position!", exception.Message);
        }

        [Fact]
        public void Run_ShouldThrowError_IfThereIsAnInvalidInstruction()
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
                {
                    { @"./randomfilePath.txt", new MockFileData(
                        "5 5" + Environment.NewLine +
                        "1 1 N" + Environment.NewLine +
                        "LRXMMM") },
                });
            var filePath = "./randomfilePath.txt";

            var nasa = new Nasa(fileSystem);
            var exception = Assert.Throws<Exception>(() => nasa.Run(filePath));

            Assert.Equal("Invalid instruction!", exception.Message);
        }

        [Fact]
        public void Run_ShouldReturnOneRover_IfOneRoversInstructionsGiven()
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
                {
                    { @"./randomfilePath.txt", new MockFileData(
                        "5 5" + Environment.NewLine +
                        "1 1 N" + Environment.NewLine +
                        "LRMMM") },
                });
            var filePath = "./randomfilePath.txt";

            var nasa = new Nasa(fileSystem);
            var result = nasa.Run(filePath);

            Assert.Single(result);
        }

        [Fact]
        public void Run_ShouldRoverBeInTheSamePosition_IfFourLeftInstructionsGiven()
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
                {
                    { @"./randomfilePath.txt", new MockFileData(
                        "5 5" + Environment.NewLine +
                        "1 1 N" + Environment.NewLine +
                        "LLLL") },
                });
            var filePath = "./randomfilePath.txt";

            var nasa = new Nasa(fileSystem);
            var result = nasa.Run(filePath);

            Assert.Single(result);

            Assert.Equal("1 1 N", result[0].ToString());
        }


        [Theory]
        [MemberData(nameof(TestInput))]
        public void Run_ShouldMoveCorrectly(string input, string expected)
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
                {
                    { @"./randomfilePath.txt", new MockFileData(input) },
                });
            var filePath = "./randomfilePath.txt";

            var nasa = new Nasa(fileSystem);
            var result = nasa.Run(filePath);

            Assert.Single(result);

            Assert.Equal(expected, result[0].ToString());
        }


        public static IEnumerable<object[]> TestInput =>
            new List<object[]>
        {
            new object[] {  "5 5" + Environment.NewLine +
                            "1 1 N" + Environment.NewLine +
                            "LLMLL", "1 0 N" },
            new object[] {  "5 5" + Environment.NewLine +
                            "0 0 E" + Environment.NewLine +
                            "MMMLMMM", "3 3 N" },
            new object[] {  "5 5" + Environment.NewLine +
                            "1 1 E" + Environment.NewLine +
                            "MMMMMMMMMMMM", "5 1 E" },
        };

    }
}
