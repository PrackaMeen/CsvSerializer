namespace Pracka.CsvSerializer.Abstractions
{
    public interface ICsvDeserializer
    {

        T GetEntityFrom<T>(string csvContent) where T : class, new();
        IEnumerable<T> GetEntitiesFrom<T>(string csvContent) where T : class, new();
    }
}
