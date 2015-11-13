using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Log4Mongo;
using log4net.Appender;
using log4net.Core;
using log4net.Util;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Charm.Logging.Mongo.Appenders
{
    public class MongoDBAppenderWithEx : AppenderSkeleton
    {
        private readonly List<MongoAppenderFileld> _fields = new List<MongoAppenderFileld>();

        public string CollectionName { get; set; }

        public string ConnectionString { get; set; }

        public void AddField(MongoAppenderFileld field)
        {
            _fields.Add(field);
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            var collection = GetCollection(CollectionName);
            Task.Run(() => collection.InsertOneAsync(BuildBsonDocument(loggingEvent)));
        }

        protected override void Append(LoggingEvent[] loggingEvents)
        {
            var collection = GetCollection(CollectionName);
            Task.Run(() => collection.InsertManyAsync(loggingEvents.Select(BuildBsonDocument)));
        }

        protected IMongoCollection<BsonDocument> GetCollection(string collectionName)
        {
            var db = GetDatabase();
            var collection = db.GetCollection<BsonDocument>(collectionName ?? "logs");
            return collection;
        }

        private IMongoDatabase GetDatabase()
        {
            if (string.IsNullOrWhiteSpace(ConnectionString))
            {
                throw new Exception("MongoDB Connection string required");
            }
            var url = MongoUrl.Create(ConnectionString);
            var mongoClient = new MongoClient(MongoClientSettings.FromUrl(url));
            var db = mongoClient.GetDatabase(url.DatabaseName ?? "log4net");
            return db;
        }

        protected BsonDocument BuildBsonDocument(LoggingEvent log)
        {
            var doc = new BsonDocument();
            foreach (var field in _fields)
            {
                var bsonValue = BsonValue.Create(String.Empty);
                if (field.Name == "properties")
                {
                    bsonValue = BuildPropertiesBsonDocument(log);
                }
                else
                {
                    var value = field.Layout.Format(log);
                    if (value is SystemStringFormat)
                    {
                        bsonValue = BsonValue.Create(value.ToString());
                    }
                    else
                    {
                        bsonValue = BsonValue.Create(value);
                    }
                }
                doc.Add(field.Name, bsonValue);

            }

            if (log.ExceptionObject != null)
            {
                doc.Add("exception", BuildExceptionBsonDocument(log.ExceptionObject));
            }
            return doc;
        }

        private BsonDocument BuildPropertiesBsonDocument(LoggingEvent loggingEvent)
        {
            var bsonDocument = new BsonDocument();
            var compositeProperties = loggingEvent.GetProperties();
            if (compositeProperties != null && compositeProperties.Count > 0)
            {
                foreach (DictionaryEntry entry in compositeProperties)
                {
                    bsonDocument[entry.Key.ToString()] = entry.Value.ToString();
                }
            }
            return bsonDocument;
        }

        private BsonDocument BuildExceptionBsonDocument(Exception ex)
        {
            var exceptionDocument = new BsonDocument {
                {"type", ex.GetType().ToString()},
                {"message", ex.Message}, 
                {"source", ex.Source}, 
                {"stackTrace", ex.StackTrace},
            };
            if (ex.InnerException != null)
            {
                exceptionDocument.Add("innerException", BuildExceptionBsonDocument(ex.InnerException));
            }
            return exceptionDocument;
        }
    }
}
