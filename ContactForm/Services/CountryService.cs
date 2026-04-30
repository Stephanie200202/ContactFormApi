using ContactForm.Dtos;
using ContactForm.Services.IServices;
using System.Net.Http;

namespace ContactForm.Services
{
    public class CountryService : ICountryService 
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CountryService> _logger;
        private readonly string _baseUrl;

        public CountryService(IHttpClientFactory httpClientFactory  , ILogger<CountryService> logger , IConfiguration configuration )
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _baseUrl = configuration["RestCountries:BaseUrl"]!;
        }

        public async Task<List<CountryDto>> GetAllCountriesAsync()
        {
            _logger.LogInformation("Fetching all countries from REST Countries API.");

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_baseUrl}/all?fields=name,capital,region,subregion,population,flags");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("REST Countries API returned {StatusCode} for GetAll.", response.StatusCode);
                return [];
            }

            var countries = await response.Content.ReadFromJsonAsync<List<RestCountryResponse>>();
            var result = countries?.Select(MapToDto).OrderBy(c => c.Name).ToList() ?? [];

            _logger.LogInformation("Fetched {Count} countries.", result.Count);
            return result;
        }

        public async Task<CountryDto?> GetCountryByNameAsync(string name)
        {
            _logger.LogInformation("Fetching country by name: {Name}", name);

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_baseUrl}/name/{Uri.EscapeDataString(name)}?fields=name,capital,region,subregion,population,flags");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Country '{Name}' not found. Status: {StatusCode}", name, response.StatusCode);
                return null;
            }

            var countries = await response.Content.ReadFromJsonAsync<List<RestCountryResponse>>();
            var country = countries?.FirstOrDefault();

            if (country == null) return null;

            _logger.LogInformation("Country '{Name}' fetched successfully.", name);
            return MapToDto(country);
        }

        private static CountryDto MapToDto(RestCountryResponse c) => new()
        {
            Name = c.Name?.Common ?? string.Empty,
            OfficialName = c.Name?.Official ?? string.Empty,
            Capital = c.Capital?.FirstOrDefault() ?? "N/A",
            Region = c.Region ?? string.Empty,
            Subregion = c.Subregion ?? "N/A",
            Population = c.Population,
            FlagUrl = c.Flags?.Png ?? string.Empty
        }; 
    }
}
