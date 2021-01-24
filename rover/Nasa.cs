using System;
using System.Collections.Generic;
using System.IO.Abstractions;

namespace rover
{
    public class Nasa
    {
        private readonly IFileSystem fileSystem;

        public Nasa(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public List<Rover> Run(string instructionsFilePath)
        {
            var inputSet = GetInputSet(instructionsFilePath);
            ParsePlateauCoordinates(inputSet[0], out int maxX, out int maxY);
            var rovers = new List<Rover>();
            for (int i = 1; i < inputSet.Length; i += 2)
            {
                GetInitialRoverPosition(inputSet[i], out int x, out int y, out Direction heading);

                var rover = new Rover(x, y, heading, maxX, maxY);
                var instructionSet = inputSet[i + 1];
                for (int j = 0; j < instructionSet.Length; j++)
                {
                    var instructionResult = Enum.TryParse(instructionSet[j].ToString(),
                        out Instruction instruction);
                    if (!instructionResult)
                    {
                        throw new Exception("Invalid instruction!");
                    }
                    rover.RunInstruction(instruction);
                }
                Console.WriteLine(rover);
                rovers.Add(rover);
            }
            return rovers;
        }

        private string[] GetInputSet(string filePath)
        {
            if (!fileSystem.File.Exists(@filePath))
            {
                throw new Exception("Instruction file not found!");
            }
            var inputSet = fileSystem.File.ReadAllLines(@filePath);
            if (inputSet.Length < 3 || inputSet.Length % 2 != 1)
            {
                throw new Exception("Invalid instruction set!");
            }
            return inputSet;
        }

        private void ParsePlateauCoordinates(string line, out int x, out int y)
        {
            var rangeSet = line.Split(" ");
            var maxXResult = int.TryParse(rangeSet[0], out x);
            var maxYResult = int.TryParse(rangeSet[1], out y);
            if (!maxXResult || !maxYResult || rangeSet.Length != 2)
            {
                throw new Exception("Invalid plateau coordinates!");
            }
        }

        private void GetInitialRoverPosition(string line, out int x, out int y, out Direction heading)
        {
            var initialPosition = line.Split(" ");
            var xResult = int.TryParse(initialPosition[0], out x);
            var yResult = int.TryParse(initialPosition[1], out y);
            var headingResult = Enum.TryParse(initialPosition[2], out heading);
            if (!xResult || !yResult || !headingResult)
            {
                throw new Exception("Invalid rover position!");
            }
        }
    }
}
