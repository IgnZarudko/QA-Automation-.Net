using System;
using System.IO;
using System.Text;

namespace UserInyerface.Tests.Util
{
    public class StringGenerator
    {
        private static string _russianAlphabet = "АаБбВвГгДдЕеЖжЗзИиЙйКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщЪъЫыЬьЭэЮюЯя";
        private static string _englishAlphabet = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsRrSsTtUuVvWwXxYyZz";
        public static string Password()
        {
            StringBuilder sb = new StringBuilder("");
            Random random = new Random(201);
            int index;
            for (int i = 0; i < 10; i++)
            {
                index = random.Next() % _englishAlphabet.Length;
                sb.Append(_englishAlphabet[index]);
            }

            index = random.Next() % _russianAlphabet.Length;
            sb.Append(_russianAlphabet[index]);
            sb.Append(random.Next() % 10);

            return sb.ToString();
        }
        
        public static string EmailPart()
        {
            return Path.GetRandomFileName().Replace(".", "");
        }
        
        
    }
    
    
}