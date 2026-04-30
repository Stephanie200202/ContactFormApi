using ContactForm.Dtos;

namespace ContactForm.Services.IServices
{
    public interface ICountryService
    {
        Task<List<CountryDto>> GetAllCountriesAsync();
        Task<CountryDto?> GetCountryByNameAsync(string name);
    }
}
