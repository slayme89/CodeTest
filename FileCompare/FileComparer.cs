using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;

namespace FileCompare
{
    public class FileComparer : IFileComparer
    {
        private readonly IFileSystem _fileSystem;

        public FileComparer() : this(new FileSystem())
        {
        }

        public FileComparer(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        /// <summary>
        /// Get a hashset of paths to files that are the same from a directory.
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public HashSet<string> GetDuplicatedFilesInDirectory(string directoryPath)
        {
            var duplicates = new HashSet<string>();
            var filesInDirectory = _fileSystem.Directory.GetFiles(directoryPath);

            for (var i = 0; i < filesInDirectory.Length - 1; i++)
            {
                for (var j = i + 1; j < filesInDirectory.Length; j++)
                {
                    if (IsTheSame(filesInDirectory[i], filesInDirectory[j]))
                    {
                        duplicates.Add(filesInDirectory[i]);
                        duplicates.Add(filesInDirectory[j]);
                    }
                }
            }

            return duplicates;
        }

        public bool IsTheSame(string firstFile, string secondFile)
        {
            return SameReference(firstFile, secondFile) || SameBinaries(firstFile, secondFile);
        }

        private bool SameBinaries(string firstFile, string secondFile)
        {
            int firstFileByte;
            int secondFileByte;
            var firstFileStream = _fileSystem.FileStream.Create(firstFile, FileMode.Open);
            var secondFileStream = _fileSystem.FileStream.Create(secondFile, FileMode.Open);

            do
            {
                firstFileByte = firstFileStream.ReadByte();
                secondFileByte = secondFileStream.ReadByte();
            } while (firstFileByte == secondFileByte && firstFileByte != -1);

            firstFileStream.Close();
            secondFileStream.Close();

            return firstFileByte - secondFileByte == 0;
        }

        private static bool SameReference(string firstFile, string secondFile)
        {
            return firstFile == secondFile;
        }
    }
}