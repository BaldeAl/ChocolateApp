using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FileServices
{
    public class JsonFileService<T> : IFileService<T>
    {
        private readonly string _filePath;

        public JsonFileService(string filePath)
        {
            _filePath = filePath;
        }

        public async Task<IEnumerable<T>> LoadAsync()
        {
            if (!File.Exists(_filePath))
            {
                return new List<T>();
            }

            using (var stream = File.OpenRead(_filePath))
            {
                return await JsonSerializer.DeserializeAsync<IEnumerable<T>>(stream) ?? new List<T>();
            }
        }

        public async Task SaveAsync(IEnumerable<T> items)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            using (var stream = File.Create(_filePath))
            {
                await JsonSerializer.SerializeAsync(stream, items, options);
            }
        }
    }
}
