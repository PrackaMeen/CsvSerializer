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
            if(0 == entity.GetType().GetProperties().Length)
            {
                return string.Empty;
            }

            throw new NotImplementedException();
        }

        public T GetEntityFrom<T>(string csvContent) where T : class, new()
        {
            throw new NotImplementedException();
        }
    }
}
