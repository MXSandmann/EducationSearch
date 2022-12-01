using EducationSearchV3.Models.Dtos.Requests;
using EducationSearchV3.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace EducationSearchV3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationProgramsController : ControllerBase
    {
        private readonly IEducationProgramService _service;

        public EducationProgramsController(IEducationProgramService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetEducationPrograms()
        {
            var programs = await _service.GetAll();
            if (programs is null)
                return NotFound("No programs in the db");
            return Ok(programs);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEducationProgramById(int id)
        {
            var program = await _service.GetById(id);
            if (program is null)
                return NotFound($"No program with id {id} could be found");
            return Ok(program);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEducationProgram(CreateUpdateEPDto dto)
        {
            var newPrograms = await _service.Create(dto);
            if (newPrograms is null)
                return BadRequest($"The program with name {dto.Name} already exist");
            return Ok(newPrograms);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEducationProgram(int id)
        {
            var newPrograms = await _service.Delete(id);
            if (newPrograms is null)
                return NotFound($"No program with id {id} could be found");
            return Ok(newPrograms);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEducationProgram(CreateUpdateEPDto dto)
        {
            var updated = await _service.Update(dto);
            if (updated is null)
                return NotFound($"The program with id {dto.Id} not found");
            return Ok(updated);
        }
    }
}
