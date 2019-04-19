using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace QTranser
{
    class Loger
    {
        public static void str(Object obj, bool IsAppend = false)
        {
            //string path = @"..\log\log.txt";
            //using (StreamWriter sw = new StreamWriter(path, IsAppend, Encoding.UTF8))
            //{
            //    sw.Write(obj);
            //    sw.Write(Environment.NewLine);
            //}
        }
        public static void json(dynamic obj, bool IsAppend = false)
        {
            string path = @"..\log\log.json";
            using (StreamWriter sw = new StreamWriter(path, IsAppend, Encoding.UTF8))
            {
                JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings { Formatting = Formatting.Indented });
                serializer.Serialize(sw, obj);
            }
        }
    }
}
