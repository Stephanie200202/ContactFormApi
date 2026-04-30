using ContactForm.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactForm.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ILogger<CountryController> _logger;
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService, ILogger<CountryController> logger)
        {
            _countryService = countryService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            _logger.LogInformation("Fetching all countries");
            var countries = await _countryService.GetAllCountriesAsync();
            return Ok(countries);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetCountryByName(string name)
        {
            _logger.LogInformation("Fetching country by name: {Name}", name);
            var country = await _countryService.GetCountryByNameAsync(name);
            if (country == null) return NotFound();
            return Ok(country);
        }

    }
}
