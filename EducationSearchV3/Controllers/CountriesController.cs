using EducationSearchV3.Models.Dtos;
using EducationSearchV3.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EducationSearchV3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryRepository _repository;

        public CountriesController(ICountryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            return Ok(await _repository.GetAll());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCountryById(int id)
        {
            var country = await _repository.GetById(id);
            if (country == null)
                return NotFound($"No country with id {id} could be found");
            return Ok(country);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCountry(CountryDto dto)
        {
            var newCountries = await _repository.Create(dto);
            if (newCountries == null)
                return BadRequest($"The country with name {dto.Name} already exists");

            return Ok(newCountries);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var newCountries = await _repository.Delete(id);

            if (newCountries == null)
                return NotFound($"The country with id {id} not found");

            return Ok(newCountries);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCountry(CountryDto dto)
        {
            var updatedSubject = await _repository.Update(dto);

            if (updatedSubject == null)
                return NotFound($"The subject with id {dto.Id} not found");

            return Ok(updatedSubject);
        }
    } 
}
