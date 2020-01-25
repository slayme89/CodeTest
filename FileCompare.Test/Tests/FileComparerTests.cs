using System.IO.Abstractions.TestingHelpers;
using AutoFixture.Xunit2;
using FileCompare.Test.AutoData;
using FluentAssertions;
using Xunit;

namespace FileCompare.Test.Tests
{
    //TODO: Create more tests..
    public class FileComparerTests
    {
        private const string Directory = @"C:\";
        
        public class GetDuplicatedFilesShould
        {
            [Theory, FileSystemData(Directory)]
            public void ReturnEmptyHashSetIfNoFilesInDirectory([Frozen]MockFileSystem mockFileSystem)
            {
                var sut = new FileComparer(mockFileSystem);

                sut.GetDuplicatedFilesInDirectory(Directory)
                    .Should().BeEmpty("Because there are no files in the given directory");
            }
            
            [Theory, FileSystemData(Directory, 2)]
            public void ReturnHashSetOfFilePaths([Frozen]MockFileSystem mockFileSystem)
            {
                var sut = new FileComparer(mockFileSystem);
                
                sut.GetDuplicatedFilesInDirectory(Directory)
                    .Should().HaveCount(2, "Because there is two files that are the same");
            }
        }

        public class IsTheSameShould
        {
            [Theory, FileData(Directory, false)]
            public void ReturnFalseIfDifferentBinaries([Frozen]MockFileSystem mockFileSystem)
            {
                var sut = new FileComparer(mockFileSystem);
                var files = mockFileSystem.Directory.GetFiles(Directory);
                
                sut.IsTheSame(files[0], files[1]).Should().BeFalse("Because the files binaries are not the same");
            }
            
            [Theory, FileData(Directory, true)]
            public void ReturnTrueIfSameBinaries([Frozen]MockFileSystem mockFileSystem)
            {
                var sut = new FileComparer(mockFileSystem);
                var files = mockFileSystem.Directory.GetFiles(Directory);
                
                sut.IsTheSame(files[0], files[1]).Should().BeTrue("Because the files have the same content");
            }
            
            [Theory, FileData(Directory, true)]
            public void ReturnTrueIfSameReference([Frozen]MockFileSystem mockFileSystem)
            {
                var sut = new FileComparer(mockFileSystem);
                var files = mockFileSystem.Directory.GetFiles(Directory);
                
                sut.IsTheSame(files[0], files[0]).Should().BeTrue("Because its the same file (same reference)");
            }
        }
    }
}