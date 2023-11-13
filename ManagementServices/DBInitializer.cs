using Models;
using System.Text.Json;

namespace ManagementServices
{
    public class DbInitializer
    {
        public static async Task InitializeAsync()
        {
            await CreateFileIfNotExistsAsync("administrators.json");
            await CreateFileIfNotExistsAsync("buyers.json");
            await CreateFileIfNotExistsAsync("articles.json");
            await CreateFileIfNotExistsAsync("purchasedArticles.json");
        }

        private static async Task CreateFileIfNotExistsAsync(string filePath)
        {
            if (!File.Exists(filePath))
            {
                using (FileStream fs = File.Create(filePath))
                {
                    var defaultData = filePath switch
                    {
                        "administrators.json" => new Administrator[] { new Administrator { Id = Guid.NewGuid(), Login = "admin", Password = "password123!" } },
                        _ => new object[0]
                    };

                    await JsonSerializer.SerializeAsync(fs, defaultData);
                }
            }
        }

    }
}