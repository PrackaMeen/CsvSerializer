namespace Pracka.CsvSerializer.Abstractions
{
    public interface ICsvSerializer
    {
        string GetCsvContentFrom<T>(T entity) where T : class, new();
        string GetCsvContentFrom<T>(IEnumerable<T> entities) where T : class, new();
    }
}
