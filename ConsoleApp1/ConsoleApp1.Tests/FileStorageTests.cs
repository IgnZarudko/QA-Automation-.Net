using ConsoleApp1.Tests.finders;
using NUnit.Framework;

namespace ConsoleApp1.Tests
{
    [TestFixture]
    public class FileStorageTests
    {
        [TestCase(123, 123.0)]
        [TestCase(11, 11.0)]
        [TestCase(-12, -12.0)]
        public static void Constructor_MaxSizeIsCorrect(int size, double maxSizeExpected)
        {
            FileStorage fileStorage = new FileStorage(size);
            Assert.AreEqual(maxSizeExpected, FileStorageInfoFinder.GetFileStorageMaxSize(fileStorage));
        }
        
        [TestCase(123, 223.0)]
        [TestCase(11, 111.0)]
        [TestCase(-12, 88.0)]
        public static void Constructor_AvailableSizeIsCorrect(int size, double availableSizeExpected)
        {
            FileStorage fileStorage = new FileStorage(size);
            Assert.AreEqual(availableSizeExpected, FileStorageInfoFinder.GetFileStorageAvailableSize(fileStorage));
        }
        
        
    }
}