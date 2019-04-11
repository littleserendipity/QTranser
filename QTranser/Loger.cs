using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTranser
{
    class Loger
    {
        public static void str(Object obj)
        {
            string path = @"C:\Users\Administrator\Desktop\log.txt";
            using (StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8))
            {
                sw.Write(obj);
                sw.Write(Environment.NewLine);
            }
        }
        public static void json(dynamic obj)
        {
            string path = @"C:\Users\Administrator\Desktop\log.json";
            using (StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8))
            {
                JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings { Formatting = Formatting.Indented });
                serializer.Serialize(sw, obj);
            }

        }
    }
}
