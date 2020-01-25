using AutoFixture;
using AutoFixture.Xunit2;
using FileCompare.Test.Customizations;

namespace FileCompare.Test.AutoData
{
    public class FileData : AutoDataAttribute
    {
        public FileData(string directory, bool isTheSame = false) :
            base(() => new Fixture().Customize(new FileDataCustomization
        {
            SameBinaries = isTheSame,
            Directory = directory
        }))
        {
        }
    }
}