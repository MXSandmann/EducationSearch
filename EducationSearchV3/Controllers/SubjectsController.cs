using EducationSearchV3.Models.Dtos.Requests;
using EducationSearchV3.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EducationSearchV3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectRepository _repository;

        public SubjectsController(ISubjectRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetSubjects()
        {
            var subjects = await _repository.GetAll();
            if(subjects is null)
                return NotFound("No subjects in the db");
            return Ok(subjects);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSubjectById(int id)
        {
            var subject = await _repository.GetById(id);
            if (subject is null)
                return NotFound($"No subject with id {id} could be found");
            return Ok(subject);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubject(CreateUpdateSubjectDto dto)
        {
            var newSubjects = await _repository.Create(dto);
            if (newSubjects is null)
                return BadRequest($"The subject with name {dto.Name} already exists");
            return Ok(newSubjects);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var newSubjects = await _repository.Delete(id);
            if (newSubjects is null)
                return NotFound($"The subject with id {id} not found");
            return Ok(newSubjects);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSubject(CreateUpdateSubjectDto dto)
        {
            var updated = await _repository.Update(dto);
            if (updated is null)
                return NotFound($"The subject with id {dto.Id} not found");
            return Ok(updated);
        }
    }
}