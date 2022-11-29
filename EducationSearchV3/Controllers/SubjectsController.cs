using EducationSearchV3.Models.Dtos.Requests;
using EducationSearchV3.Services;
using Microsoft.AspNetCore.Mvc;

namespace EducationSearchV3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _service;

        public SubjectsController(ISubjectService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetSubjects()
        {
            var subjects = await _service.GetAll();
            if(subjects is null)
                return NotFound("No subjects in the db");
            return Ok(subjects);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSubjectById(int id)
        {
            var subject = await _service.GetById(id);
            if (subject is null)
                return NotFound($"No subject with id {id} could be found");
            return Ok(subject);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubject(CreateUpdateSubjectDto dto)
        {
            var newSubjects = await _service.Create(dto);
            if (newSubjects is null)
                return BadRequest($"The subject with name {dto.Name} already exists");
            return Ok(newSubjects);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var newSubjects = await _service.Delete(id);
            if (newSubjects is null)
                return NotFound($"The subject with id {id} not found");
            return Ok(newSubjects);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSubject(CreateUpdateSubjectDto dto)
        {
            var updated = await _service.Update(dto);
            if (updated is null)
                return NotFound($"The subject with id {dto.Id} not found");
            return Ok(updated);
        }
    }
}