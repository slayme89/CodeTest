using System;
using FileCompare;

namespace ConsoleApp
{
    class Program
    {
        private static void Main()
        {
            var comparer = new FileComparer();

            string directory;
            do
            {
                Console.WriteLine("Directory path: ");
                directory = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(directory))
                    Console.WriteLine($"{directory} is not a valid directory, try again!");

            } while (string.IsNullOrWhiteSpace(directory));

            var duplicatedFiles = comparer.GetDuplicatedFilesInDirectory(directory);

            Console.WriteLine($"Duplicated files in {directory}:");
            
            foreach (var file in duplicatedFiles)
            {
                Console.WriteLine($"File: {file.Substring(directory.Length)} Path: {file}");
            }
        }
    }
}
