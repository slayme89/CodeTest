using System.Collections.Generic;

namespace FileCompare
{
    public interface IFileComparer
    {
        bool IsTheSame(string firstFilePath, string secondFilePath);
        HashSet<string> GetDuplicatedFilesInDirectory(string directoryPath);
    }
}