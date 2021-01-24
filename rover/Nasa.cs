using System;
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

        public void Run(string instructionsFilePath)
        {
            if (!fileSystem.File.Exists(@instructionsFilePath))
            {
                throw new Exception("Instruction file not found!");
            }
            var inputSet = fileSystem.File.ReadAllLines(@instructionsFilePath);
            if (inputSet.Length < 3 || inputSet.Length % 2 != 1)
            {
                throw new Exception("Invalid instruction set!");
            }
            var rangeSet = inputSet[0].Split(" ");
            var maxXResult = int.TryParse(rangeSet[0], out int  maxX);
            var maxYResult = int.TryParse(rangeSet[1], out int  maxY);
            if(!maxXResult || !maxYResult || rangeSet.Length != 2)
            {
                throw new Exception("Invalid plateau coordinates!");
            }

            for (int i = 1; i < inputSet.Length; i += 2)
            {
                var initialPosition = inputSet[i].Split(" ");
                var xResult = int.TryParse(initialPosition[0], out int x);
                var yResult = int.TryParse(initialPosition[1], out int y);
                var headingResult = Enum.TryParse(initialPosition[2], out Direction heading);
                if (!xResult || !yResult || !headingResult)
                {
                    throw new Exception("Invalid rover position!");
                }
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
            }
            Console.WriteLine("Finished");
        }
    }
}
