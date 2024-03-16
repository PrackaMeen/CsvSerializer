using Pracka.CsvSerializer.Abstractions;
using System.Text;

namespace Pracka.CsvSerializer
{
    public class CsvSerializer : ICsvSerializer, ICsvDeserializer
    {
        public string GetCsvContentFrom<T>(T? entity) where T : class, new()
        {
            if (null == entity)
            {
                return GetCsvHeader(new T());
            }

            if (0 == entity.GetType().GetProperties().Length)
            {
                return string.Empty;
            }

            var contentHeader = GetCsvHeader(entity);
            var contentBody = GetCsvBody(entity);
            return $"{contentHeader}{Environment.NewLine}{contentBody}";
        }

        public string GetCsvContentFrom<T>(IEnumerable<T> entities) where T : class, new()
        {
            var contentHeader = GetCsvHeader(new T());
            if (0 == entities.Count())
            {
                return contentHeader;
            }

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(contentHeader);

            foreach (var entity in entities)
            {
                stringBuilder.AppendLine(GetCsvBody(entity));
            }

            return stringBuilder.ToString();
        }

        public string GetCsvHeader<T>(T entity) where T : class, new()
        {
            var propertyNames = entity
                  .GetType()
                  .GetProperties()
                  .Select((property) => property.Name);

            if (propertyNames.Any())
            {
                return propertyNames
                    .Aggregate((previous, current) => $"{previous},{current}");
            }

            return string.Empty;
        }

        public string GetCsvBody<T>(T entity) where T : class, new()
        {
            var propertyValues = entity
                  .GetType()
                  .GetProperties()
                  .Select((property) => property.GetValue(entity))
                  .Select(GetValueAsString);

            if (propertyValues.Any())
            {
                return propertyValues
                    .Aggregate((previous, current) => $"{previous},{current}");
            }

            return string.Empty;
        }

        public string GetValueAsString(object? value)
        {
            if (value is null)
            {
                return string.Empty;
            }

            return value.ToString();
        }

        public T GetEntityFrom<T>(string csvContent) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetEntitiesFrom<T>(string csvContent) where T : class, new()
        {
            throw new NotImplementedException();
        }
    }
}
