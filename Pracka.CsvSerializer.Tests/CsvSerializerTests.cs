using Pracka.CsvSerializer.Abstractions;

namespace Pracka.CsvSerializer.Tests
{
    public class CsvSerializerTests
    {
        [Fact]
        public void GetCsvContentFrom_ForNotNullEntity()
        {
            ICsvSerializer csvSerializer = new CsvSerializer();

            var entityWithoutProperties = new EntityWithoutProperties();
            var content = csvSerializer.GetCsvContentFrom(entityWithoutProperties);


            Assert.NotNull(entityWithoutProperties);
            Assert.Equal(content, string.Empty);
        }

        private class EntityWithoutProperties
        {

        }
    }
}