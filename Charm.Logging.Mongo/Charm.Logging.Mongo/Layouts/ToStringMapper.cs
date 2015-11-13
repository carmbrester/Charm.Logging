using MongoDB.Bson;

namespace Charm.Logging.Mongo.Layouts
{
    public class ToStringMapper : ICustomBsonTypeMapper
    {
        public bool TryMapToBsonValue(object value, out BsonValue bsonValue)
        {
            if (value != null)
            {
                bsonValue = new BsonString(value.ToString());
            }
            else
            {
                bsonValue = new BsonString(string.Empty);
            }
            return true;
        }
    }
}
