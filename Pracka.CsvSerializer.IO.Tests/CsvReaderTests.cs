using Xunit;

namespace Pracka.CsvSerializer.IO.Tests
{
    public class CsvReaderTests
    {
        private static string GetAbsolutePathFrom(string relativePath)
        {
            var combinedPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
            return Path.GetFullPath(combinedPath);
        }

        [Fact]
        public void File_Not_Exists_Should_Throw_FileNotFoundException()
        {
            var fullPath = GetAbsolutePathFrom("./filePath2.csv");

            Assert.False(File.Exists(fullPath), "File should not be existing");
            Assert.Throws<FileNotFoundException>(() => new CsvReader(fullPath));
        }

        //[Fact]
        //public async Task File_Read_Should_Throw_NotImplementedException()
        //{
        //    var fullPath = GetAbsolutePathFrom("./files/testEntity.csv");
        //    Assert.True(File.Exists(fullPath), "File should be existing");

        //    using (var csvWriter = new CsvReader(fullPath))
        //    {
        //        await Assert.ThrowsAsync<NotImplementedException>(csvWriter.ReadEntitiesAsync<TestEntity>);
        //    }
        //}

        class TestEntity
        {
            public int? IntProp { get; set; }
            public string StringProp { get; set; }
            public double? DoubleProp { get; set; }
            public decimal? DecimalProp { get; set; }
        }
    }
}
