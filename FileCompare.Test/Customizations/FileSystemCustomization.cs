using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using AutoFixture;

namespace FileCompare.Test.Customizations
{
    public class FileSystemCustomization : ICustomization
    {
        public string Directory;
        public bool NoFiles;
        public int NumberOfDuplicates;
        private static Random _random;
        private const int MaxNumberOfFiles = 100; //So the tests dosent take too long..
        
        public void Customize(IFixture fixture)
        {
            var mockFileSystem = new MockFileSystem();

            if (NoFiles)
            {
                fixture.Inject(mockFileSystem);
                return;
            }

            _random = new Random();
            var listOfFiles = new List<MockFileData>();
            var numberOfFiles = _random.Next(NumberOfDuplicates, MaxNumberOfFiles);
            var duplicatedMockFileData = new MockFileData(fixture.Create<string>());

            if (NumberOfDuplicates < 0 || NumberOfDuplicates >= MaxNumberOfFiles)
                NumberOfDuplicates = numberOfFiles;

            for (var i = 0; i < NumberOfDuplicates; i++)
            {
                listOfFiles.Add(duplicatedMockFileData);
            }

            var numberOfNonDuplicates = numberOfFiles - NumberOfDuplicates;

            for (var j = 0; j < numberOfNonDuplicates; j++)
            {
                listOfFiles.Add(new MockFileData(fixture.Create<string>()));
            }

            foreach (var file in listOfFiles)
            {
                mockFileSystem.AddFile(Directory + fixture.Create<string>(), file);
            }

            fixture.Inject(mockFileSystem);
        }
    }
}