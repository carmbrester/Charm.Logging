using System;
using System.Collections.Specialized;
using FluentAssertions;
using Charm.Logging.Mongo.Layouts;
using MongoDB.Bson;
using NUnit.Framework;

namespace Charm.Logging.Mongo.Tests.Layouts
{
    [TestFixture]
    public class MessagePackDataLayoutTests
    {
        [Test]
        public void Format_DynamicDataObjectWithOneProperty_ShouldReturnDocumentWithOneProperty()
        {
            var data = new{First = 1};
            var document = MessagePackDataLayout.ConvertToBsonDocument(data);
            document.Should().NotBeNull();
            document.ElementCount.Should().Be(1);
        }

        [Test]
        public void Format_DynamicDataContainsInt_ShouldReturnDocumentWithOneProperty()
        {
            var data = 15;
            var document = MessagePackDataLayout.ConvertToBsonDocument(data);
            document.Should().NotBeNull();
            document.ElementCount.Should().Be(1);
        }

        [Test]
        public void Format_DynamicDataContainsString_ShouldReturnDocumentWithOneProperty()
        {
            var data = "string";
            var document = MessagePackDataLayout.ConvertToBsonDocument(data);
            document.Should().NotBeNull();
            document.ElementCount.Should().Be(1);
        }

        [Test]
        public void Format_DynamicDataContainsDecimal_ShouldReturnDocumentWithOneProperty()
        {
            var data = 4.5m;
            var document = MessagePackDataLayout.ConvertToBsonDocument(data);
            document.Should().NotBeNull();
            document.ElementCount.Should().Be(1);
        }

        [Test]
        public void Format_DynamicDataContainsDateTime_ShouldReturnDocumentWithOneProperty()
        {
            var data = DateTime.Now;
            var document = MessagePackDataLayout.ConvertToBsonDocument(data);
            document.Should().NotBeNull();
            document.ElementCount.Should().Be(1);
        }

        [Test]
        public void Format_DynamicDataContainsDouble_ShouldReturnDocumentWithOneProperty()
        {
            var data = DateTime.Now;
            var document = MessagePackDataLayout.ConvertToBsonDocument(data);
            document.Should().NotBeNull();
            document.ElementCount.Should().Be(1);
        }

        [Test]
        public void Format_DynamicDataContainsFloat_ShouldReturnDocumentWithOneProperty()
        {
            var data = 4.5f;
            var document = MessagePackDataLayout.ConvertToBsonDocument(data);
            document.Should().NotBeNull();
            document.ElementCount.Should().Be(1);
        }

        [Test]
        public void Format_DynamicDataContainsTimespan_ShouldReturnDocumentWithOneProperty()
        {
            var data = TimeSpan.FromDays(DateTime.Now.Day);
            var document = MessagePackDataLayout.ConvertToBsonDocument(data);
            document.Should().NotBeNull();
            document.ElementCount.Should().Be(1);
        }

        [Test]
        public void Format_DynamicDataContainsNameValueCollection_ShouldReturnDocumentWithPropertyCountEqualToCollectionCount()
        {
            var collection = new NameValueCollection();
            collection.Add("one", "1");
            collection.Add("two", "2");
            collection.Add("three", "3");
            var document = MessagePackDataLayout.ConvertToBsonDocument(collection);
            document.Should().NotBeNull();
            document.ElementCount.Should().Be(collection.Count);

            document.GetElement("one").Value.Should().Be("1");
        }

        [Test]
        public void Format_DynamicDataContainsDynamic_ShouldReturnDocumentWithPropertyCountEqualToDynamicPropertyCount()
        {
            var dy = new
            {
                One = 1,
                Two = "two",
                Three = new object(),
            };
            var document = MessagePackDataLayout.ConvertToBsonDocument(dy);
            document.Should().NotBeNull();
            document.ElementCount.Should().Be(3);
        }

        [Test]
        public void Format_DynamicDataContainsDynamicWithNestedDynamic_ShouldReturnDocumentWithSubdocument()
        {
            var dy = new
            {
                One = 1,
                Two = "two",
                Three = new
                {
                    SubOne = "S1",
                    SubTwo = new object(),
                },
            };
            var document = MessagePackDataLayout.ConvertToBsonDocument(dy);
            document.Should().NotBeNull();
            document.ElementCount.Should().Be(3);
        }

        [Test]
        public void Format_DynamicDataContainsDynamicWithNameValueCollection_ShouldReturnDocumentWithSubDocumentPropertiesEqualToNameValueCollection()
        {
            var collection = new NameValueCollection();
            collection.Add("un", "1");
            collection.Add("deux", "2");
            collection.Add("trois", "3");
            var dy = new
            {
                One = 1,
                Two = "two",
                Three = new object(),
                Coll = collection,
            };
            var document = MessagePackDataLayout.ConvertToBsonDocument(dy);
            document.Should().NotBeNull();

            ((BsonDocument)document.GetElement("Coll").Value).GetElement("un").Value.Should().Be("1");
        }

        [Test]
        public void Format_DynamicDataContainsPropertyWithJsonArray_ShouldReturnDocumentWithSubdocument()
        {
            var data = new {Body = "[{\"Name\":\"First\"},{\"Name\":\"Second\"}]",Headers="Some Data"};
            var document = MessagePackDataLayout.ConvertToBsonDocument(data);
            document.Should().NotBeNull();
            document.ElementCount.Should().Be(2);
            BsonElement dummy;
            document.TryGetElement("MessagePackDataLayoutError", out dummy).Should().BeFalse();
        }
    }
}
