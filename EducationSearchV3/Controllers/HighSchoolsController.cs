using EducationSearchV3.Models.Dtos;
using EducationSearchV3.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EducationSearchV3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HighSchoolsController : ControllerBase
    {
        private readonly IHighSchoolRepository _repository;
        public HighSchoolsController(IHighSchoolRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateHighSchool(HighSchoolDto dto)
        {
            var newHighSchools = await _repository.Create(dto);
            if (newHighSchools == null)
                return BadRequest($"The HighSchool {dto.Name} already exists");
            return Ok(newHighSchools);

        }
    }
}
