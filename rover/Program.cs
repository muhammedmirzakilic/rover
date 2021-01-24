using System;
using System.IO.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace rover
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IFileSystem, FileSystem>()
                .AddSingleton<Nasa>()
                .BuildServiceProvider();

            var instructionsFilePath = @"./instructions.txt";
            var nasa = serviceProvider.GetService<Nasa>();
            nasa.Run(instructionsFilePath);
        }
    }
}
