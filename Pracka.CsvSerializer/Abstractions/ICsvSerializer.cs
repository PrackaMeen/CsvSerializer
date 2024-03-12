namespace Pracka.CsvSerializer.Abstractions
{
    public interface ICsvSerializer
    {
        string GetCsvContentFrom<T>(T entity) where T : class, new();
        T GetEntityFrom<T>(string csvContent) where T : class, new();
    }
}
