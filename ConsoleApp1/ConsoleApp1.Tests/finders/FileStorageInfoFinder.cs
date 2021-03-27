using System;
using System.Reflection;

namespace ConsoleApp1.Tests.finders
{
    public static class FileStorageInfoFinder
    {
        public static double GetFileStorageMaxSize(FileStorage fileStorage)
        {
            FieldInfo fieldInfo = typeof(FileStorage).GetField("maxSize", BindingFlags.Instance | BindingFlags.NonPublic);
            return (double)fieldInfo?.GetValue(fileStorage);
        }
        
        public static double GetFileStorageAvailableSize(FileStorage fileStorage)
        {
            FieldInfo fieldInfo = typeof(FileStorage).GetField("availableSize", BindingFlags.Instance | BindingFlags.NonPublic);
            return (double)fieldInfo?.GetValue(fileStorage);
        }
    }
}