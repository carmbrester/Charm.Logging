using FluentAssertions;
using Charm.Logging.Mongo.Layouts;
using log4net.Core;
using log4net.Util;
using MongoDB.Bson;
using NUnit.Framework;

namespace Charm.Logging.Mongo.Tests.Layouts
{
    [TestFixture]
    public class PropertyBagLayoutTests
    {
        [Test]
        public void Format_FailedJsonSerializationProperty_ShouldNotObstructOtherPropertiesFromBeingLogged()
        {
            var layout = new PropertyBagLayout();
            var loggingEventData = new LoggingEventData {Properties = new PropertiesDictionary()};
            loggingEventData.Properties["AcceptableProperty"] = "Just some data to be logged";
            loggingEventData.Properties["AnonymousObjectToString"] = new
            {
                FirstProperty = "Some data.",
                SecondProperty = 123,
            }.ToString();
            var loggingEvent = new LoggingEvent(loggingEventData);
            var output = layout.Format(loggingEvent);

            output.GetType().Should().Be(typeof (BsonDocument));
            ((BsonDocument) output).Names.Should().Contain("AcceptableProperty");
        }
    }
}
