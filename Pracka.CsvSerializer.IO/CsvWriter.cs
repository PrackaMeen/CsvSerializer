using Pracka.CsvSerializer.Abstractions;
using System.Reflection.Metadata;
using System.Text;

namespace Pracka.CsvSerializer.IO
{
    public class CsvWriter : IDisposable, IAsyncDisposable
    {
        private bool disposedValue;
        private readonly StreamWriter _writer;
        private readonly ICsvSerializer _serializer;

        public CsvWriter(string filePath)
        {
            _writer = new StreamWriter(filePath);
            _serializer = new CsvSerializer();
        }

        public async Task WriteEntityAsync<T>(T entity) where T : class, new()
        {
            var fileContent = _serializer.GetCsvContentFrom(entity);
            await _writer.WriteAsync(fileContent);
            await _writer.FlushAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    _writer.Close();
                    _writer.Dispose();
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
            return ((IAsyncDisposable)_writer).DisposeAsync();
        }
    }
}
