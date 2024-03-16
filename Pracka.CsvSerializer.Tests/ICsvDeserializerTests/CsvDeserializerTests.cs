using Newtonsoft.Json.Linq;
using Pracka.CsvSerializer.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace Pracka.CsvSerializer.Tests.ICsvDeserializerTests
{
    public class CsvDeserializerTests
    {
        [Fact]
        public void Throw_ArgumentException_For_Not_Valid_Content()
        {
            ICsvDeserializer csvSerializer = new CsvSerializer();

            Assert.Throws<ArgumentException>(
                () => csvSerializer.GetEntityFrom<EntityWithoutProperties>(string.Empty)
            );
        }

        [Fact]
        public void Has_Valid_Content()
        {
            CsvSerializer csvSerializer = new CsvSerializer();
            var entityToTest = new EntityWithoutProperties();
            var entityToTestContent = entityToTest.GetContent();

            Assert.False(
                csvSerializer.IsContentInvalidEntity<EntityWithoutProperties>(entityToTestContent),
                "Entity header should be valid"
            );
        }

        [Fact]
        public void EntityWithoutProperties_Is_Deserialized_Successfully()
        {
            ICsvDeserializer csvSerializer = new CsvSerializer();

            var expectedEntity = new EntityWithoutProperties();
            var content = expectedEntity.GetContent();

            var actualEntity = csvSerializer.GetEntityFrom<EntityWithoutProperties>(content);

            Assert.NotNull(actualEntity);
            Assert.Equal(expectedEntity, actualEntity, new EntityEqualityComparer());
        }

        [Fact]
        public void EntityWithExactOneProperty_Is_Deserialized_Successfully()
        {
            var expectedEntity = new EntityWithExactOneProperty();
            var content = expectedEntity.GetContent();

            ICsvDeserializer csvSerializer = new CsvSerializer();
            var actualEntity = csvSerializer.GetEntityFrom<EntityWithExactOneProperty>(content);

            Assert.NotNull(expectedEntity);
            Assert.Equal(expectedEntity, actualEntity, new EntityEqualityComparer());
        }

        [Fact]
        public void EntityWithMultipleProperties_Is_Deserialized_Successfully()
        {
            var expectedEntity = new EntityWithMultipleProperties();
            var content = expectedEntity.GetContent();

            ICsvDeserializer csvSerializer = new CsvSerializer();
            var actualEntity = csvSerializer.GetEntityFrom<EntityWithMultipleProperties>(content);

            Assert.NotNull(expectedEntity);
            Assert.Equal(expectedEntity, actualEntity, new EntityEqualityComparer());
        }

        [Fact]
        public void EntityWithMultipleProperties_With_Set_Properties_Is_Deserialized_Successfully()
        {
            var expectedEntity = new EntityWithMultipleProperties()
            {
                Property1 = 1,
                Property2 = 2,
                Property3 = null
            };
            var content = expectedEntity.GetContent();

            ICsvDeserializer csvSerializer = new CsvSerializer();
            var actualEntity = csvSerializer.GetEntityFrom<EntityWithMultipleProperties>(content);

            Assert.NotNull(expectedEntity);
            Assert.Equal(expectedEntity, actualEntity, new EntityEqualityComparer());
        }

        private class EntityWithoutProperties : TestEntity
        {

        }

        private class EntityWithExactOneProperty : TestEntity
        {
            public decimal? Property { get; set; }

            public override string GetHeaderLine()
            {
                return $"{nameof(Property)}";
            }

            public override string GetBodyLine()
            {
                return $"{Property}";
            }
        }

        private class EntityWithMultipleProperties : TestEntity
        {
            public decimal? Property1 { get; set; }
            public decimal Property2 { get; set; }
            public decimal? Property3 { get; set; }

            public override string GetHeaderLine()
            {
                return $"{nameof(Property1)},{nameof(Property2)},{nameof(Property3)}";
            }

            public override string GetBodyLine()
            {
                return $"{Property1},{Property2},{Property3}";
            }
        }

        private interface ICsvDeserializable
        {
            string GetHeaderLine();
            string GetBodyLine();
            string GetContent();
        }

        private abstract class TestEntity : ICsvDeserializable
        {
            public virtual string GetBodyLine()
            {
                return string.Empty;
            }

            public virtual string GetHeaderLine()
            {
                return string.Empty;
            }

            public string GetContent()
            {
                return $"{GetHeaderLine()}{Environment.NewLine}{GetBodyLine()}";
            }
        }

        private class EntityEqualityComparer : EqualityComparer<TestEntity>
        {
            public override bool Equals(TestEntity? x, TestEntity? y)
            {
                if (x is null) return false;
                if (y is null) return false;

                return x.GetContent().Equals(y.GetContent());
            }

            public override int GetHashCode([DisallowNull] TestEntity obj)
            {
                return obj.GetHashCode();
            }
        }

    }
}