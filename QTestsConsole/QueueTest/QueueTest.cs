using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QTests
{
    class QueueTest
    {
        public static void Main1()
        {
            var dm = new DocumentManager();

            ProcessDocuments.StartAsyc(dm);

            for (int i = 0; i < 1000; i++)
            {
                var doc = new Document($"Doc {i.ToString()}", "Content");
                dm.AddDocument(doc);
                Console.WriteLine($"Added Document { doc.Title}");
                Thread.Sleep(new Random().Next(20));
            }
            //Console.ReadKey();
        }
    }
}
