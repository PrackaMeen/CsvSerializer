using Pracka.CsvSerializer.Abstractions;

namespace Pracka.CsvSerializer.Tests
{
    public class CsvSerializerDecimalPropertiesTests
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

            var entityWithProperties = new EntityWithExactOneProperty();
            var content = csvSerializer.GetCsvContentFrom(entityWithProperties);

            Assert.NotNull(content);
            Assert.NotEqual(string.Empty, content);
        }

        [Fact]
        public void Entity_With_Exact_One_Property_Has_Content()
        {
            ICsvSerializer csvSerializer = new CsvSerializer();

            var entityWithOneProperty = new EntityWithExactOneProperty();
            var content = csvSerializer.GetCsvContentFrom(entityWithOneProperty);

            var lines = content.Split(Environment.NewLine);

            int numberOfExpectedLines = 2;
            Assert.Equal(numberOfExpectedLines, lines.Length);
        }

        [Fact]
        public void Entity_With_Multiple_Properties_Has_Content()
        {
            ICsvSerializer csvSerializer = new CsvSerializer();

            var entityWithMultipleProperties = new EntityWithMultipleProperties();
            var content = csvSerializer.GetCsvContentFrom(entityWithMultipleProperties);

            var lines = content.Split(Environment.NewLine);

            int numberOfExpectedLines = 2;
            Assert.Equal(numberOfExpectedLines, lines.Length);
        }

        [Fact]
        public void Entity_With_Exact_One_Default_Property_Content_Header_Is_Valid()
        {
            ICsvSerializer csvSerializer = new CsvSerializer();

            var entityWithExactOneProperty = new EntityWithExactOneProperty();

            var content = csvSerializer.GetCsvContentFrom(entityWithExactOneProperty);
            var contentHeader = content.Split(Environment.NewLine)[0];

            var expectedContentHeader = $"Property";

            Assert.NotNull(content);
            Assert.Equal(expectedContentHeader, contentHeader);
        }

        [Fact]
        public void Entity_With_Exact_One_Initialized_Property_Content_Header_Is_Valid()
        {
            ICsvSerializer csvSerializer = new CsvSerializer();

            var entityWithExactOneProperty = new EntityWithExactOneProperty()
            {
                Property = decimal.MinValue
            };

            var content = csvSerializer.GetCsvContentFrom(entityWithExactOneProperty);
            var contentHeader = content.Split(Environment.NewLine)[0];

            var expectedContentHeader = $"Property";

            Assert.NotNull(content);
            Assert.Equal(expectedContentHeader, contentHeader);
        }

        [Fact]
        public void Entity_With_Multiple_Default_Properties_Content_Header_Is_Valid()
        {
            ICsvSerializer csvSerializer = new CsvSerializer();

            var entityWithMultipleProperties = new EntityWithMultipleProperties();

            var content = csvSerializer.GetCsvContentFrom(entityWithMultipleProperties);
            var contentHeader = content.Split(Environment.NewLine)[0];

            var expectedContentHeader = $"Property1,Property2,Property3";

            Assert.NotNull(content);
            Assert.Equal(expectedContentHeader, contentHeader);
        }

        [Fact]
        public void Entity_With_Multiple_Initialized_Properties_Content_Header_Is_Valid()
        {
            ICsvSerializer csvSerializer = new CsvSerializer();

            var entityWithMultipleProperties = new EntityWithMultipleProperties()
            {
                Property1 = 1.234M,
                Property2 = 2.34M,
                Property3 = null
            };

            var content = csvSerializer.GetCsvContentFrom(entityWithMultipleProperties);
            var contentHeader = content.Split(Environment.NewLine)[0];

            var expectedContentHeader = $"Property1,Property2,Property3";

            Assert.NotNull(content);
            Assert.Equal(expectedContentHeader, contentHeader);
        }

        [Fact]
        public void Entity_With_Exact_One_Default_Property_Content_Is_Valid()
        {
            ICsvSerializer csvSerializer = new CsvSerializer();

            var entityWithExactOneProperty = new EntityWithExactOneProperty();

            var content = csvSerializer.GetCsvContentFrom(entityWithExactOneProperty);

            var expectedContent = $"Property{Environment.NewLine}";

            Assert.NotNull(content);
            Assert.Equal(expectedContent, content);
        }

        [Fact]
        public void Entity_With_Exact_One_Initialized_Property_Content_Is_Valid()
        {
            ICsvSerializer csvSerializer = new CsvSerializer();

            var entityWithExactOneProperty = new EntityWithExactOneProperty()
            {
                Property = decimal.MinValue
            };

            var content = csvSerializer.GetCsvContentFrom(entityWithExactOneProperty);

            var expectedContent = $"Property{Environment.NewLine}{decimal.MinValue}";

            Assert.NotNull(content);
            Assert.Equal(expectedContent, content);
        }

        [Fact]
        public void Entity_With_Multiple_Default_Properties_Content_Is_Valid()
        {
            ICsvSerializer csvSerializer = new CsvSerializer();

            var entityWithMultipleProperties = new EntityWithMultipleProperties();

            var content = csvSerializer.GetCsvContentFrom(entityWithMultipleProperties);

            var expectedContent = $"Property1,Property2,Property3{Environment.NewLine},0,";

            Assert.NotNull(content);
            Assert.Equal(expectedContent, content);
        }

        [Fact]
        public void Entity_With_Multiple_Initialized_Properties_Content_Is_Valid()
        {
            ICsvSerializer csvSerializer = new CsvSerializer();

            var entityWithMultipleProperties = new EntityWithMultipleProperties()
            {
                Property1 = 1.234M,
                Property2 = 2.34M,
                Property3 = null
            };

            var content = csvSerializer.GetCsvContentFrom(entityWithMultipleProperties);

            var expectedContent = $"Property1,Property2,Property3{Environment.NewLine}1.234,2.34,";

            Assert.NotNull(content);
            Assert.Equal(expectedContent, content);
        }

        [Fact]
        public void GetValueAsString_Is_Valid()
        {
            decimal? valueToTest = 1M;
            CsvSerializer csvSerializer = new CsvSerializer();
            var stringValue = csvSerializer.GetValueAsString(valueToTest);

            Assert.Equal("1", stringValue);
        }

        [Fact]
        public void GetValueAsString_Null_Is_Valid()
        {
            decimal? valueToTest = null;
            CsvSerializer csvSerializer = new CsvSerializer();
            var stringValue = csvSerializer.GetValueAsString(valueToTest);

            Assert.Equal(string.Empty, stringValue);
        }

        private class EntityWithoutProperties
        {

        }

        private class EntityWithExactOneProperty
        {
            public decimal? Property { get; set; }
        }

        private class EntityWithMultipleProperties
        {
            public decimal? Property1 { get; set; }
            public decimal Property2 { get; set; }
            public decimal? Property3 { get; set; }
        }
    }
}