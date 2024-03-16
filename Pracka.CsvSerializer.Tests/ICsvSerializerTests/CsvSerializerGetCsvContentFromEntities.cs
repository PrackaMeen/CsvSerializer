using Pracka.CsvSerializer.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pracka.CsvSerializer.Tests.ICsvSerializerTests
{
    public class CsvSerializerGetCsvContentFromEntities
    {
        [Fact]
        public void Null_Entity_Has_Only_Header()
        {
            ICsvSerializer csvSerializer = new CsvSerializer();

            EntityWithAllAllowedTypes nullEntity = null;
            var content = csvSerializer.GetCsvContentFrom<EntityWithAllAllowedTypes>(nullEntity);
            var lines = content.Split(Environment.NewLine);
            var numberOfContentLines = lines.Length;
            var header = lines[0];

            var expectedNumberOfLinesInContent = 1;
            var expectedHeader = "IntProperty,DoubleProperty,DecimalProperty,StringProperty";

            Assert.NotNull(content);
            Assert.Equal(expectedNumberOfLinesInContent, numberOfContentLines);
            Assert.Equal(expectedHeader, header);
        }

        [Fact]
        public void Empty_List_Has_Only_Header()
        {
            ICsvSerializer csvSerializer = new CsvSerializer();

            List<EntityWithAllAllowedTypes> entities = new List<EntityWithAllAllowedTypes>();
            var content = csvSerializer.GetCsvContentFrom<EntityWithAllAllowedTypes>(entities);
            var lines = content.Split(Environment.NewLine);
            var numberOfContentLines = lines.Length;
            var header = lines[0];

            var expectedNumberOfLinesInContent = 1;
            var expectedHeader = "IntProperty,DoubleProperty,DecimalProperty,StringProperty";

            Assert.NotNull(content);
            Assert.Equal(expectedNumberOfLinesInContent, numberOfContentLines);
            Assert.Equal(expectedHeader, header);
        }

        class EntityWithAllAllowedTypes
        {
            public int? IntProperty { get; set; }
            public double? DoubleProperty { get; set; }
            public decimal? DecimalProperty { get; set; }
            public string? StringProperty { get; set; }
        }
    }
}
