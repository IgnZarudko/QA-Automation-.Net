using System.Collections.Generic;
using ConsoleApp1.Tests.finders;
using FileSystem.exception;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
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
            Assert.AreEqual(maxSizeExpected, FileStorageInfoFinder.GetFileStorageMaxSize(fileStorage), $"Wrong max size, expected {maxSizeExpected}");
        }
        
        [TestCase(123, 223.0)]
        [TestCase(11, 111.0)]
        [TestCase(-12, 88.0)]
        public static void Constructor_AvailableSizeIsCorrect(int size, double availableSizeExpected)
        {
            FileStorage fileStorage = new FileStorage(size);
            Assert.AreEqual(availableSizeExpected, FileStorageInfoFinder.GetFileStorageAvailableSize(fileStorage), $"Wrong max size, expected {availableSizeExpected}");
        }

        private static IEnumerable<TestCaseData> WriteTestCaseData
        {
            get
            {
                yield return new TestCaseData(new FileStorage(10), new File("new-file-to-empty.md", "kek"), true);
                
                FileStorage fileStorage = new FileStorage();
                fileStorage.write(new File("file.txt",
                    "123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890"));
                yield return new TestCaseData(fileStorage, new File("new-file-to-not-empty.md", "kek"), true);
                
                yield return new TestCaseData(new FileStorage(), new File("new-file-to-empty.md", "too big for this storage!too big for this storage!too big for this storage!too big for this storage!too big for this storage!too big for this storage!too big for this storage!too big for this storage!"), false);
                
                fileStorage = new FileStorage();
                fileStorage.write(new File("file.txt",
                    "123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890"));
                yield return new TestCaseData(fileStorage, new File("new-file-to-not-empty.md", "too big for this storage!too big for this storage!too big for this storage!too big for this storage!too big for this storage!"), false);

            }
        }
        
        [TestCaseSource(typeof(FileStorageTests),nameof(WriteTestCaseData))]
        public static void Write_ReturnsCorrectResult(FileStorage storage, File file, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, storage.write(file), $"something went wrong while writing file \'{file.getFilename()}\' (expected {expectedResult}, but found {!expectedResult})");
        }

        private static IEnumerable<TestCaseData> WriteThrowsExceptionData
        {
            get
            {
                FileStorage fileStorage = new FileStorage();
                fileStorage.write(new File("file.txt",
                    "123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890"));
                yield return new TestCaseData(fileStorage, new File("file.txt", "it doesn't matter what inside"));
            }
        }
        
        [TestCaseSource(typeof(FileStorageTests),nameof(WriteThrowsExceptionData))]
        public static void Write_ThrowsFileNameAlreadyExistsException(FileStorage storage, File file)
        {
            Assert.Throws<FileNameAlreadyExistsException>(
                () => storage.write(file), $"{file.getFilename()} was added, but it should not have"
                );
        }

        private static IEnumerable<TestCaseData> IsExistsData
        {
            get
            {
                FileStorage fileStorage = new FileStorage();
                yield return new TestCaseData(fileStorage, "full-name.txt", false);
                
                fileStorage = new FileStorage();
                fileStorage.write(new File("full-name.txt", "content"));
                yield return new TestCaseData(fileStorage, "full-name.txt", true);
                yield return new TestCaseData(fileStorage, "full-name", true);
                yield return new TestCaseData(fileStorage, "name", true);
                yield return new TestCaseData(fileStorage, "some-name.txt", false);
            }
        }

        [TestCaseSource(typeof(FileStorageTests), nameof(IsExistsData))]
        public static void IsExists_ReturnsCorrectResult(FileStorage fileStorage, string filename, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, fileStorage.isExists(filename), $"Got error searching {filename}");
        }
        
        private static IEnumerable<TestCaseData> DeleteData
        {
            get
            {
                FileStorage fileStorage = new FileStorage();
                yield return new TestCaseData(fileStorage, "file.txt", false);
                
                fileStorage = new FileStorage();
                fileStorage.write(new File("file.txt", "content"));
                fileStorage.write(new File("some-file.txt", "some content"));
                yield return new TestCaseData(fileStorage, "file.txt", true);
                yield return new TestCaseData(fileStorage, "some-file.txt", true);
                yield return new TestCaseData(fileStorage, "fiel.ttx", false);
            }
        }
        
        [TestCaseSource(typeof(FileStorageTests), nameof(DeleteData))]
        public static void Delete_ReturnsCorrectResult(FileStorage fileStorage, string filename, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, fileStorage.delete(filename));
        }
        
    }
}