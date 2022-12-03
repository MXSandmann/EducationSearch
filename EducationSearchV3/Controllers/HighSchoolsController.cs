using EducationSearchV3.Models.Dtos.Requests;
using EducationSearchV3.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EducationSearchV3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HighSchoolsController : ControllerBase
    {
        private readonly IHighSchoolService _service;
        public HighSchoolsController(IHighSchoolService service)
        {            
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetHighSchools()
        {
            var schools = await _service.GetAll();
            if (schools is null)
                return NotFound($"No schools in the db");
            return Ok(schools);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetHighSchoolById(int id)
        {
            var school = await _service.GetById(id);
            if (school is null)
                return NotFound($"No school with id {id} could be found");
            return Ok(school);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHighSchool(CreateUpdateHighSchoolDto dto)
        {
            var newSchools = await _service.Create(dto);
            if (newSchools == null)
                return BadRequest($"The HighSchool {dto.Name} already exists");
            return Ok(newSchools);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteHighSchool(int id)
        {
            var newSchools = await _service.Delete(id);
            if (newSchools is null)
                return NotFound($"The HighSchool with id {id} not found");
            return Ok(newSchools);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHighSchool(CreateUpdateHighSchoolDto dto)
        {
            var updated = await _service.Update(dto);
            if (updated is null)
                return NotFound($"The HighSchool with id {dto.Id} not found");
            return Ok(updated);
        }
    }
}
