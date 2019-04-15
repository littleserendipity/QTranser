using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTranser.QTranseLib.MongoDB
{
    public class Credentials
    {
        public  Credentials(string historyWord)
        {
            string username = "xyfRW";
            string password = "yangqiRW";
            string mongoDbAuthMechanism = "SCRAM-SHA-1";
            MongoInternalIdentity internalIdentity = new MongoInternalIdentity("tests", username);
            PasswordEvidence passwordEvidence = new PasswordEvidence(password);
            MongoCredential mongoCredential = new MongoCredential(mongoDbAuthMechanism, internalIdentity, passwordEvidence);
            String mongoHost = "47.95.197.94";
            MongoServerAddress address = new MongoServerAddress(mongoHost);
            MongoClientSettings settings = new MongoClientSettings();
            settings.Credential = mongoCredential;
            settings.Server = address;

            string st = "mongodb://xyfRW:yangqiRW@47.95.197.94:27017/tests?authSource=tests";

            MongoClient client = new MongoClient(st);

            var mongoServer = client.GetDatabase("tests");
            var coll = mongoServer.GetCollection<HistoryWord>("xyf");

            // any stubbed out class
            HistoryWord history = new HistoryWord()
            {
                Word = historyWord
            };

            coll.InsertOne(history);
        }
    }
}
