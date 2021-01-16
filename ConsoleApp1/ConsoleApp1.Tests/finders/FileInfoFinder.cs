using System;
using System.Reflection;

namespace ConsoleApp1.Tests.finders
{
    public static class FileInfoFinder
    {
        public static string GetFileName(File file)
        {
            Type fileType = file.GetType();
            FieldInfo fieldInfo = typeof(File).GetField("filename", BindingFlags.Instance | BindingFlags.NonPublic);
            return (string)fieldInfo?.GetValue(file);
        }

        public static string GetFileContent(File file)
        {
            Type fileType = file.GetType();
            FieldInfo fieldInfo = typeof(File).GetField("content", BindingFlags.Instance | BindingFlags.NonPublic);
            return (string)fieldInfo?.GetValue(file);
        }

        public static string GetFileExtension(File file)
        {
            Type fileType = file.GetType();
            FieldInfo fieldInfo = typeof(File).GetField("extension", BindingFlags.Instance | BindingFlags.NonPublic);
            return (string)fieldInfo?.GetValue(file);
        }

        public static double GetFileSize(File file)
        {
            Type fileType = file.GetType();
            FieldInfo fieldInfo = typeof(File).GetField("size", BindingFlags.Instance | BindingFlags.NonPublic);
            return (double)fieldInfo.GetValue(file);
        }
    }
}