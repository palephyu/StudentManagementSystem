using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace StudentManagementSystem.Services
{
    public class WeatherDto
    {
        public string City { get; set; } = "";
        public double Temperature { get; set; }
        public double FeelsLike { get; set; }
        public int Humidity { get; set; }
        public string Weather { get; set; } = "";
        public string Icon { get; set; } = "";
    }

    public class WeatherApiService
    {
        private readonly HttpClient _http;
        private readonly string _studentApiBase; // StudentApi base url

        public WeatherApiService(HttpClient http, IConfiguration config)
        {
            _http = http;
            // Provide StudentApi base URL in MVC appsettings (see step 6)
            _studentApiBase = config["StudentApi:BaseUrl"]?.TrimEnd('/') ?? "https://localhost:7139";
        }

        public async Task<WeatherDto?> GetWeatherAsync(string city)
        {
            var url = $"{_studentApiBase}/api/weather/{Uri.EscapeDataString(city)}";
            var resp = await _http.GetAsync(url);
            if (!resp.IsSuccessStatusCode) return null;

            var json = await resp.Content.ReadAsStringAsync();
            var dto = JsonSerializer.Deserialize<WeatherDto>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return dto;
        }
    }
}
