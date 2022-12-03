using EducationSearchV3.Models.Dtos.Requests;
using EducationSearchV3.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EducationSearchV3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            var countries = await _countryService.GetAll();
            if (countries is null) 
                return NotFound($"No countries in the db");
            return Ok(countries);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCountryById(int id)
        {
            var country = await _countryService.GetById(id);
            if (country is null) 
                return NotFound($"No country with id {id} could be found");
            return Ok(country);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCountry(CreateUpdateCountryDto dto)
        {
            var newCountries = await _countryService.Create(dto);
            if (newCountries is null) 
                return BadRequest($"The country with name {dto.Name} already exists");
            return Ok(newCountries);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var newCountries = await _countryService.Delete(id);
            if (newCountries is null) 
                return NotFound($"The country with id {id} not found");
            return Ok(newCountries);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCountry(CreateUpdateCountryDto dto)
        {
            var updated = await _countryService.Update(dto);
            if (updated is null) 
                return NotFound($"The country with id {dto.Id} not found");
            return Ok(updated);
        }
    } 
}
