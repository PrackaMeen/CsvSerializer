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
            if (IsContentInvalidEntity<T>(csvContent))
            {
                throw new ArgumentException($"Argument \"{nameof(csvContent)}\" is not of type \"{nameof(T)}\"");
            }

            return GetEntitiesFrom<T>(csvContent).First();
        }

        public IEnumerable<T> GetEntitiesFrom<T>(string csvContent) where T : class, new()
        {
            if (IsContentInvalidEntity<T>(csvContent))
            {
                throw new ArgumentException($"Argument \"{nameof(csvContent)}\" is not of type \"{nameof(T)}\"");
            }

            if (string.IsNullOrEmpty(csvContent))
            {
                return [new T()];
            }

            return new T[] {
                GetCreatedEntityFrom<T>(csvContent)
            };
        }

        public bool IsContentInvalidEntity<T>(string content) where T : class, new()
        {
            if (!content.Contains(Environment.NewLine))
            {
                return true;
            }

            var expectedHeader = GetCsvHeader<T>(new T());
            var contentHeader = content.Split(Environment.NewLine).First();


            return !expectedHeader.Equals(contentHeader);
        }

        public T GetCreatedEntityFrom<T>(string csvContent) where T : class, new()
        {
            var entity = new T();

            var lines = csvContent.Split(Environment.NewLine);
            var header = lines.First();
            var content = lines.Skip(1).First();
            var entityData = GetEntityDictionaryFrom(header, content);

            var allEntityProperties = entity.GetType().GetProperties();
            foreach (var property in allEntityProperties)
            {
                if (entityData.TryGetValue(property.Name, out string value))
                {
                    if (property.PropertyType == typeof(int))
                    {
                        property.SetValue(entity, int.Parse(value));
                    }
                    else if (property.PropertyType == typeof(double))
                    {
                        property.SetValue(entity, double.Parse(value));
                    }
                    else if (property.PropertyType == typeof(decimal))
                    {
                        property.SetValue(entity, GetParsedDecimalValue(value));
                    }
                    else if (property.PropertyType == typeof(decimal?))
                    {
                        property.SetValue(entity, GetParsedNullableDecimalValue(value));
                    }
                    else if (property.PropertyType is string)
                    {
                        property.SetValue(entity, value);
                    }
                }
            }

            return entity;
        }

        public decimal? GetParsedNullableDecimalValue(string? value)
        {
            decimal? newPropertyValue = null;
            if (!string.IsNullOrEmpty(value))
            {
                newPropertyValue = (decimal?)decimal.Parse(value);
            }

            return newPropertyValue;
        }

        public decimal GetParsedDecimalValue(string? value)
        {
            decimal newPropertyValue = default(decimal);
            if (!string.IsNullOrEmpty(value))
            {
                newPropertyValue = decimal.Parse(value);
            }

            return newPropertyValue;
        }

        public T SetProperty<T>(T entity, string propertyName, string value) where T : class, new()
        {
            foreach (var property in entity.GetType().GetProperties())
            {
                if (property.Name.Equals(propertyName))
                {
                    property.SetValue(entity, value);
                }
            }

            return entity;
        }

        public string[] GetCsvValuesFrom(string csvContentLine)
        {
            return csvContentLine.Split(",");
        }

        public IDictionary<string, string> GetEntityDictionaryFrom(
            string csvContentHeader,
            string csvContentBody
        )
        {
            var propertiesNames = GetCsvValuesFrom(csvContentHeader);
            var propertiesValues = GetCsvValuesFrom(csvContentBody);

            if (propertiesNames.Length != propertiesValues.Length)
            {
                throw new ArgumentException(
                    $"\"{propertiesValues}\" has different number of items in compare with \"{csvContentHeader}\""
                );
            }

            var entityDictionary = new Dictionary<string, string>();
            for (int i = 0; i < propertiesNames.Length; i++)
            {
                entityDictionary.Add(propertiesNames[i], propertiesValues[i]);
            }

            return entityDictionary;
        }
    }
}
