using Pracka.CsvSerializer.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pracka.CsvSerializer
{
    public class CsvSerializer : ICsvSerializer
    {
        public string GetCsvContentFrom<T>(T entity) where T : class, new()
        {
            if (0 == entity.GetType().GetProperties().Length)
            {
                return string.Empty;
            }

            var contentHeader = GetCsvHeader(entity);
            var contentBody = GetCsvBody(entity);
            return $"{contentHeader}{Environment.NewLine}{contentBody}";
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
    }
}
