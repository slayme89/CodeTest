using AutoFixture;
using AutoFixture.Xunit2;
using FileCompare.Test.Customizations;

namespace FileCompare.Test.AutoData
{
    public class FileSystemData : AutoDataAttribute
    {
        public FileSystemData(string directory, int numberOfDuplicates = -1, bool containNoFiles = false) :
            base(() => new Fixture().Customize(new FileSystemCustomization
        {
            Directory = directory,
            NumberOfDuplicates = numberOfDuplicates,
            NoFiles = containNoFiles
        }))
        {
        }
    }
}