using System.IO.Abstractions.TestingHelpers;
using AutoFixture;

namespace FileCompare.Test.Customizations
{
    public class FileDataCustomization : ICustomization
    {
        public bool SameBinaries;
        public string Directory;
        
        public void Customize(IFixture fixture)
        {
            var mockFileSystem = new MockFileSystem();
            var mockFileData = new MockFileData(fixture.Create<string>());
            
            mockFileSystem.AddFile(Directory + fixture.Create<string>(), mockFileData);
            
            if (SameBinaries)
            {
                mockFileSystem.AddFile(Directory + fixture.Create<string>(), mockFileData);
                fixture.Inject(mockFileSystem);
                return;
            }
            
            mockFileSystem.AddFile(Directory + fixture.Create<string>(), new MockFileData(fixture.Create<string>()));
            fixture.Inject(mockFileSystem);
        }
    }
}