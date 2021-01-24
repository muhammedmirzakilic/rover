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
            var maxX = Convert.ToInt32(rangeSet[0]);
            var maxY = Convert.ToInt32(rangeSet[1]);

            for (int i = 1; i < inputSet.Length; i += 2)
            {
                var initialPosition = inputSet[i].Split(" ");
                var x = Convert.ToInt32(initialPosition[0]);
                var y = Convert.ToInt32(initialPosition[1]);
                _ = Enum.TryParse(initialPosition[2], out Direction heading);
                var rover = new Rover(x, y, heading, maxX, maxY);
                var instructionSet = inputSet[i + 1];
                for (int j = 0; j < instructionSet.Length; j++)
                {
                    _ = Enum.TryParse(instructionSet[j].ToString(), out Instruction instruction);
                    rover.RunInstruction(instruction);
                }
                Console.WriteLine(rover);
            }
            Console.WriteLine("Finished");
            Console.ReadLine();
        }
    }
}
