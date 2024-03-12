using Pracka.CsvSerializer.Abstractions;

namespace Pracka.CsvSerializer.Tests
{
    public class CsvSerializerTests
    {
        [Fact]
        public void Not_Null_Entity_Without_Properties_Has_Empty_Content()
        {
            ICsvSerializer csvSerializer = new CsvSerializer();

            var entityWithoutProperties = new EntityWithoutProperties();
            var content = csvSerializer.GetCsvContentFrom(entityWithoutProperties);

            Assert.NotNull(entityWithoutProperties);
            Assert.Equal(string.Empty, content);
        }

        [Fact]
        public void Not_Null_Entity_With_Properties_Has_Not_Empty_Content()
        {
            ICsvSerializer csvSerializer = new CsvSerializer();

            var entityWithProperties = new EntityWithProperties();
            var content = csvSerializer.GetCsvContentFrom(entityWithProperties);

            Assert.NotNull(content);
            Assert.NotEqual(string.Empty, content);
        }

        [Fact]
        public void Entity_With_Exact_One_Property_Has_Content()
        {
            ICsvSerializer csvSerializer = new CsvSerializer();

            var entityWithOneProperty = new EntityWithProperties();
            var content = csvSerializer.GetCsvContentFrom(entityWithOneProperty);

            var lines = content.Split(Environment.NewLine);

            int numberOfExpectedLines = 2;
            Assert.Equal(numberOfExpectedLines, lines.Length);
        }

        private class EntityWithoutProperties
        {

        }

        private class EntityWithProperties
        {
            public string Property { get; set; }
        }
    }
}