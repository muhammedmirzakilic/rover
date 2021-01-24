using System;
using System.IO;
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
            var currentPath = AppDomain.CurrentDomain.BaseDirectory;
            var instructionsFilePath = Path.Combine(currentPath, "instructions.txt");
            var nasa = serviceProvider.GetService<Nasa>();
            nasa.Run(instructionsFilePath);
        }
    }
}
