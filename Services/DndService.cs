using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DndExplorer.Services
{
    public class DndService
    {
        private readonly HttpClient _httpClient;
        public DndService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ApiResult>> GetApiListAsync(string category)
        {
            var response = await _httpClient.GetFromJsonAsync<ApiListResponse>($"https://www.dnd5eapi.co/api/{category}");
            return response?.Results ?? new List<ApiResult>();
        }

        public async Task<ApiDetail> GetApiDetailAsync(string category, string index)
        {
            // Fetch the JSON string from the API
            var jsonString = await _httpClient.GetStringAsync($"https://www.dnd5eapi.co/api/{category}/{index}");

            // Parse the JSON
            var jsonDocument = JsonDocument.Parse(jsonString);
            var data = jsonDocument.RootElement;

            // Create the ApiDetail object and populate it
            var detail = new ApiDetail
            {
                Name = data.GetProperty("name").GetString(),
                Properties = new Dictionary<string, string>()
            };

            foreach (var property in data.EnumerateObject())
            {
                if (property.Name != "name")
                {
                    detail.Properties[property.Name] = property.Value.ToString();
                }
            }

            return detail;
        }
    }

    public class ApiListResponse
    {
        public List<ApiResult> Results { get; set; }
    }

    public class ApiResult
    {
        public string Index { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class ApiDetail
    {
        public string Name { get; set; }
        public Dictionary<string, string> Properties { get; set; } = new();
    }
}
