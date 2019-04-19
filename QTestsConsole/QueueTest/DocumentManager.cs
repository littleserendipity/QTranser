using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTests
{
    public class DocumentManager
    {
        private readonly Queue<Document> _documentQueue = new Queue<Document>();

        public void AddDocument(Document doc)
        {
            lock (this)
            {
                _documentQueue.Enqueue(doc);
            }
        }
        public Document GetDocument()
        {
            Document doc = null;
            lock (this)
            {
                doc = _documentQueue.Dequeue();
            }
            return doc;
        }
        public bool IsDocumentAvailable => _documentQueue.Count > 0;
    }
}
