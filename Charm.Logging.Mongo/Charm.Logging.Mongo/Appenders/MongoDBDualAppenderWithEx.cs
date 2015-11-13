using System.Linq;
using System.Threading.Tasks;
using log4net.Core;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Charm.Logging.Mongo.Appenders
{
    public class MongoDBDualAppenderWithEx : MongoDBAppenderWithEx
    {
        public string DualCollectionName { get; set; }
        public Level DualCollectionLevelThreshold { get; set; }

        protected override void Append(LoggingEvent loggingEvent)
        {
            var collection = GetCollection(CollectionName);
            IMongoCollection<BsonDocument> dualCollection = null;
            if (!string.IsNullOrWhiteSpace(DualCollectionName))
            {
                dualCollection = GetCollection(DualCollectionName);
            }
            Task.Run(() =>
            {
                var doc = BuildBsonDocument(loggingEvent);
                collection.InsertOneAsync(doc);
                if (dualCollection != null && loggingEvent.Level >= DualCollectionLevelThreshold)
                {
                    dualCollection.InsertOneAsync(doc);
                }
            });
        }

        protected override void Append(LoggingEvent[] loggingEvents)
        {
            var collection = GetCollection(CollectionName);
            IMongoCollection<BsonDocument> dualCollection = null;
            if (!string.IsNullOrWhiteSpace(DualCollectionName))
            {
                dualCollection = GetCollection(DualCollectionName);
            }
            Task.Run(() =>
            {
                collection.InsertManyAsync(loggingEvents.Select(BuildBsonDocument));
                dualCollection?.InsertManyAsync(
                    loggingEvents.Where(log => log.Level >= DualCollectionLevelThreshold).Select(BuildBsonDocument));
            });
        }
    }
}
