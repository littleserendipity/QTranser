using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace QTranser
{
    class Loger
    {
        public static void Exception(Object obj, bool IsAppend = false)
        {
            string path = @"..\log";
            string dirc = @"..\log\Exception.txt";
            CreateDirectory(path);
            using (StreamWriter sw = new StreamWriter(dirc, IsAppend, Encoding.UTF8))
            {
                sw.Write(obj);
                sw.Write(Environment.NewLine);
            }
        }
        public static void json(dynamic obj, bool IsAppend = false)
        {
            string path = @"..\log";
            string dirc = @"..\log\log.json";

            CreateDirectory(path);
           
            using (StreamWriter sw = new StreamWriter(dirc, IsAppend, Encoding.UTF8))
            {
                JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings { Formatting = Formatting.Indented });
                serializer.Serialize(sw, obj);
            }
        }
        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

    }
}
