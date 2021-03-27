using ConsoleApp1.Tests.finders;
using NUnit.Framework;

namespace ConsoleApp1.Tests
{
    [TestFixture]
    public class FileTests
    {
        [TestCase("filename.extension", "some strange content, does not matter.")]
        [TestCase("file-without-extension", "this file has ho extension")]
        [TestCase(".gitignore", "*/idea")]
        [TestCase("", "absolutely empty name")]
        public void Constructor_NameIsCorrect(string filename, string content)
        {
            File file = new File(filename, content);
            Assert.AreEqual(filename, FileInfoFinder.GetFileName(file), $"Wrong filename, expected \'{filename}\'");
        }

        [TestCase("filename.extension", "some strange content, does not matter.")]
        [TestCase(".gitignore", "*/idea")]
        [TestCase("empty-content-file.txt", "")]
        public void Constructor_ContentIsCorrect(string filename, string content)
        {
            File file = new File(filename, content);
            Assert.AreEqual(content, FileInfoFinder.GetFileContent(file), $"Wrong content, expected \'{content}\'");
        }

        [TestCase("filename.extension", "some strange content, does not matter.", 19.0)]
        [TestCase(".gitignore", "*/idea", 3.0)]
        [TestCase("Multi-line.txt", "some\nlines\ncontent", 9.0)]
        [TestCase("odd-content-size.txt", "odd content size!", 8.5)]
        [TestCase("empty-content-file.txt", "", 0.0)]
        public void Constructor_SizeIsCorrect(string filename, string content, double expectedSize)
        {
            File file = new File(filename, content);
            Assert.AreEqual(expectedSize, FileInfoFinder.GetFileSize(file), $"Wrong size, expected {expectedSize}");
        }

        [TestCase("filename.extension", "some strange content, does not matter.", "extension")]
        [TestCase("file-without-extension", "this file has ho extension", "")]
        [TestCase(".gitignore", "#Has 'gitignore' extension\n*/idea", "gitignore")]
        [TestCase("", "no file extension", "")]
        public void Constructor_ExtensionIsCorrect(string filename, string content, string expectedExtension)
        {
            File file = new File(filename, content);
            Assert.AreEqual(expectedExtension, FileInfoFinder.GetFileExtension(file), $"Wrong extension, expected \'{expectedExtension}\'");
        }

        [TestCase("filename.extension", "some strange content, does not matter.", 19.0)]
        [TestCase(".gitignore", "*/idea", 3.0)]
        [TestCase("Multi-line.txt", "some\nlines\ncontent", 9.0)]
        [TestCase("odd-content-size.txt", "odd content size!", 8.5)]
        [TestCase("empty-content-file.txt", "", 0.0)]
        public void GetSize_ReturnsCorrectSize(string filename, string content, double expectedSize)
        {
            File file = new File(filename, content);
            Assert.AreEqual(expectedSize, file.getSize(), $"Getter returned wrong size, expected {expectedSize}");
        }

        [TestCase("filename.extension", "some strange content, does not matter.")]
        [TestCase("file-without-extension", "this file has ho extension")]
        [TestCase(".gitignore", "*/idea")]
        [TestCase("", "absolutely empty name")]
        public void GetFileName_ReturnsCorrectName(string filename, string content)
        {
            File file = new File(filename, content);
            Assert.AreEqual(filename, FileInfoFinder.GetFileName(file), $"Getter returned wrong filename, expected \'{filename}\'");
        }
        
    }
}