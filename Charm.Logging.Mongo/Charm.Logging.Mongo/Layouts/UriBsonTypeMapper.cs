using System;
using MongoDB.Bson;

namespace Charm.Logging.Mongo.Layouts
{
    /// <summary>
    /// The System.Uri type does not automatically map to a Bson type and throws exceptions when attempted.
    /// Likely all we care about is the url, so that's what we're outputting with this custom mapper.
    /// </summary>
    public class UriBsonTypeMapper : ICustomBsonTypeMapper
    {
        public bool TryMapToBsonValue(object value, out BsonValue bsonValue)
        {
            var uri = value as Uri;
            if (uri != null)
            {
                bsonValue = new BsonString(uri.AbsoluteUri);
                return true;
            }
            bsonValue = new BsonString(string.Empty);
            return false;
        }
    }
}