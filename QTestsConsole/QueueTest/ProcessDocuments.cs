using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace QTests
{
    public class ProcessDocuments
    {
        public static void StartAsyc(DocumentManager dm)
        {
            Task.Run(new ProcessDocuments(dm).Run);
        }

        protected ProcessDocuments(DocumentManager dm)
        {
            if (dm == null)
                throw new ArgumentException("dm");
            _documentManager = dm;
        }

        private DocumentManager _documentManager;

        protected async Task Run()
        {
            while(true)
            {
                if (_documentManager.IsDocumentAvailable)
                {
                    Document doc = _documentManager.GetDocument();
                    WriteLine($"Processing document {doc.Title}");
                }
                await Task.Delay(new Random().Next(2000));
            }
        }
    }
}
