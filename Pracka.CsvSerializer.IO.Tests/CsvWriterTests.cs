using Xunit;

namespace Pracka.CsvSerializer.IO.Tests
{
    public class CsvWriterTests
    {
        private static string GetAbsolutePathFrom(string relativePath)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
        }

        [Fact]
        public async Task File_Created_Successfully()
        {
            var fullPath = GetAbsolutePathFrom("./filePath.csv");
            Assert.False(File.Exists(fullPath), "File should not be existing yet");

            using (var csvWriter = new CsvWriter(fullPath))
            {
                await csvWriter.WriteEntityAsync(new TestEntity());
            }

            Assert.True(File.Exists(fullPath), "File should now exist");
            File.Delete(fullPath);
            Assert.False(File.Exists(fullPath), "File should be no longer existing");
        }

        [Fact]
        public async Task Test_File_Creation()
        {
            var fullPath = GetAbsolutePathFrom("./filePath.csv");
            Assert.False(File.Exists(fullPath), "File should not be existing yet");

            using (var csvWriter = new CsvWriter(fullPath))
            {
                await csvWriter.WriteEntityAsync(new TestEntity());
            }

            Assert.True(File.Exists(fullPath), "File should now exist");
            File.Delete(fullPath);
            Assert.False(File.Exists(fullPath), "File should be no longer existing");
        }

        class TestEntity
        {
            public int? IntProp { get; set; }
            public string StringProp { get; set; }
            public double? DoubleProp { get; set; }
            public decimal? DecimalProp { get; set; }
        }
    }
}
