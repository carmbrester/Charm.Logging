using System;
using System.Collections.Generic;
using System.Linq;
using log4net.Core;
using log4net.Layout;
using MongoDB.Bson;

namespace Charm.Logging.Mongo.Layouts
{
    /// <summary>
    /// Custom layout for outputting the property bag without duplicating some of the properties
    /// that we're already pulling out of the property bag and logging at the top level. Profile,
    /// for example, nearly doubles the size of our mongo logs because of the duplication in our log entries.
    /// </summary>
    /// <remarks>
    /// Note that the field name when configured for Log4Mongo should _not_ use any of the built in
    /// log4net pattern layout identifiers - in particular "properties" or "p". Doing so will override
    /// this implementation and use log4net's pattern layout instead.
    /// </remarks>
    public class PropertyBagLayout : IRawLayout
    {
        private IList<string> _keys = new List<string>();

        public object Format(LoggingEvent loggingEvent)
        {
            try
            {
                var propertyBag = GetProperties(loggingEvent);
                var document = new BsonDocument();
                var propertyBagKeys = propertyBag.Keys;
                foreach (var key in propertyBagKeys)
                {
                    if (!_keys.Contains(key))
                    {
                        BsonValue bsonVal;
                        if (BsonTypeMapper.TryMapToBsonValue(propertyBag[key], out bsonVal))
                        {
                            if (bsonVal is BsonString && IsJson(bsonVal.AsString))
                            {
                                try
                                {
                                    document.Add(key,
                                        MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(
                                            bsonVal.AsString));
                                }
                                catch (Exception ex)
                                {
                                    document.Add("PropertyBagLayoutError", ex.ToString());
                                    document.Add("PropertyBagLayoutErrorData", bsonVal);
                                    document.Add(key, bsonVal);
                                }
                            }
                            else
                            {
                                document.Add(key, bsonVal);
                            }
                        }
                        else
                        {
                            var tostring = propertyBag[key].ToString();
                            if (IsJson(tostring))
                            {
                                try
                                {
                                    document.Add(key,
                                        MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(tostring));
                                }
                                catch (Exception ex)
                                {
                                    document.Add("PropertyBagLayoutError", ex.ToString());
                                    document.Add("PropertyBagLayoutErrorData", tostring);
                                    document.Add(key, tostring);
                                }
                            }
                            else
                            {
                                document.Add(key, new BsonString(tostring));
                            }
                        }
                    }
                }

                return document;
            }
            catch (Exception ex)
            {
                var document = new BsonDocument
                {
                    {"PropertyBagLayoutFatalError", ex.ToString()}
                };
                return document;
            }
        }

        protected virtual IDictionary<string, object> GetProperties(LoggingEvent loggingEvent)
        {
            var properties = new Dictionary<string, object>();
            var log4netProperties = loggingEvent.GetProperties();
            foreach (var key in log4netProperties.GetKeys())
            {
                properties.Add(key, log4netProperties[key]);
            }
            return properties;
        }

        public void AddKeys(string keys)
        {
            _keys = keys == null
                ? new List<string>()
                : keys.Split(new[] { "," }, StringSplitOptions.None).ToList();
        }

        public static bool IsJson(string input)
        {
            input = input.Trim();
            return input.StartsWith("{") && input.EndsWith("}")
                   || input.StartsWith("[") && input.EndsWith("]");
        }
    }
}