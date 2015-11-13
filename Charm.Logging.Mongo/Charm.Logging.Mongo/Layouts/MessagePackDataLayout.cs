using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using log4net.Core;
using log4net.Layout;
using MongoDB.Bson;

namespace Charm.Logging.Mongo.Layouts
{
    public class MessagePackDataLayout : IRawLayout
    {
        public object Format(LoggingEvent loggingEvent)
        {
            try
            {
                var messagePack = loggingEvent.MessageObject as MessagePack;
                if (messagePack == null || messagePack.Data == null)
                {
                    return null;
                }

                return ConvertToBsonDocument(messagePack.Data);
            }
            catch (Exception ex)
            {
                var document = new BsonDocument
                {
                    {"MessagePackDataLayoutFatalError", ex.ToString()}
                };
                return document;
            }
        }

        public static BsonDocument ConvertToBsonDocument(dynamic data)
        {
            var document = new BsonDocument();
            var type = (Type) data.GetType();
            var props = type.GetProperties();
            if (type.IsValueType || type.IsPrimitive || type == typeof(string) || type == typeof(decimal) || !props.Any())
            {
                AddToDocument("[Raw]", data, document);
            }
            else if (type == typeof(NameValueCollection))
            {
                var collection = data as NameValueCollection;
                foreach (var key in collection.AllKeys)
                {
                    AddToDocument(key, collection[key], document);
                }
            }
            else
            {
                var pairs =
                    props.Select(x => new KeyValuePair<string, object>(x.Name, x.GetValue(data, null))).ToArray();
                foreach (var pair in pairs)
                {
                    if (pair.Value != null && pair.Value.GetType().IsAnonymousType())
                    {
                        var subdocument = ConvertToBsonDocument(pair.Value);
                        document.Add(pair.Key, subdocument);
                    }
                    else
                    {
                        AddToDocument(pair.Key, pair.Value, document);
                    }
                }
            }
            return document;
        }

        private static void AddToDocument(string key, object data, BsonDocument document)
        {
            BsonValue bsonVal;
            if (data is NameValueCollection)
            {
                var collection = data as NameValueCollection;
                var subdocument = new BsonDocument();
                foreach (var subkey in collection.AllKeys)
                {
                    subdocument.Add(subkey, collection[subkey]);
                }
                document.Add(key, subdocument);
            }
            else if (BsonTypeMapper.TryMapToBsonValue(data, out bsonVal))
            {
                if (bsonVal is BsonString)
                {
                    if (IsJsonArray(bsonVal.AsString))
                    {
                        try
                        {
                            document.Add(key,
                                MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonArray>(
                                    bsonVal.AsString));
                        }
                        catch (Exception ex)
                        {
                            AddMessagePackException(key, document, ex, bsonVal);
                        }
                    }
                    else if (IsJson(bsonVal.AsString))
                    {
                        try
                        {
                            document.Add(key,
                                MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(
                                    bsonVal.AsString));
                        }
                        catch (Exception ex)
                        {
                            AddMessagePackException(key, document, ex, bsonVal);
                        }
                    }
                    else
                    {
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
                var tostring = data.ToString();
                if (IsJsonArray(tostring))
                {
                    try
                    {
                        document.Add(key,
                            MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonArray>(
                                bsonVal.AsString));
                    }
                    catch (Exception ex)
                    {
                        AddMessagePackException(key, document, ex, bsonVal);
                    }
                }
                else if (IsJson(tostring))
                {
                    try
                    {
                        document.Add(key,
                            MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(tostring));
                    }
                    catch (Exception ex)
                    {
                        AddMessagePackException(key, document, ex, tostring);
                    }
                }
                else
                {
                    try
                    {
                        document.Add(key, data.ToBsonDocument());
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            document.Add(key, BsonTypeMapper.MapToBsonValue(tostring));
                        }
                        catch (Exception)
                        {
                            // This catch is probably redundant - leaving it in place just to be paranoid about catching all logging.
                            AddMessagePackException(key, document, ex, tostring);
                        }
                    }
                }
            }
        }

        private static void AddMessagePackException(string key, BsonDocument document, Exception ex, BsonValue bsonVal)
        {
            document.Add("MessagePackDataLayoutError", ex.ToString());
            document.Add("MessagePackDataLayoutErrorData", bsonVal);
            document.Add(key, bsonVal);
        }

        public static bool IsJson(string input)
        {
            input = input.Trim();
            return input.StartsWith("{") && input.EndsWith("}");
        }

        public static bool IsJsonArray(string input)
        {
            input = input.Trim();
            // checking a few known special cases.
            return (input.StartsWith("[") && input.EndsWith("]") && (input != "[NULL]" && input != "[EMPTY]"));
        }
    }
}
