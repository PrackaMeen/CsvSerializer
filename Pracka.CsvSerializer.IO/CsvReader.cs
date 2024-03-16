using Pracka.CsvSerializer.Abstractions;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Text;

namespace Pracka.CsvSerializer.IO
{
    public class CsvReader : IDisposable, IAsyncDisposable
    {
        private bool disposedValue;
        private readonly StreamReader _reader;
        private readonly ICsvDeserializer _deserializer;

        public CsvReader(string filePath)
        {
            _reader = new StreamReader(filePath);
            _deserializer = new CsvSerializer();
        }

        public async Task<IEnumerable<T>> ReadEntitiesAsync<T>() where T : class, new()
        {
            var fileContent = await _reader.ReadToEndAsync();
            var entities = _deserializer.GetEntitiesFrom<T>(fileContent);
            
            return entities;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    _reader.Close();
                    _reader.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~CsvWriter()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        ValueTask IAsyncDisposable.DisposeAsync()
        {
            return ((IAsyncDisposable)_reader).DisposeAsync();
        }
    }
}
