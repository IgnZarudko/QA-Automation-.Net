using System.Collections.Generic;
using System.IO;

namespace HelloWebdriver.Tests.util
{
    public static class CsvSerializer
    {
        public static void WriteToFile(string path, List<(string name, string url)> categories)
        {
            using StreamWriter file = new StreamWriter(Path.GetFullPath(path));

            foreach (var tuple in categories)
            {
                string data = tuple.name + ',' + tuple.url;
                file.WriteLine(data);
            }
        }
    }
}