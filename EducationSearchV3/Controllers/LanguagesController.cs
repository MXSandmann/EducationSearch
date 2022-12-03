using EducationSearchV3.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EducationSearchV3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LanguagesController : ControllerBase
    {
        private readonly ILanguageService _service;
        public LanguagesController(ILanguageService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLanguages()
        {
            var languages = await _service.GetAll();
            if (languages is null)
                return NotFound("No languages in db");
            return Ok(languages);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetLanguageById(int id)
        {
            var language = await _service.GetById(id);
            if (language is null)
                return NotFound($"No language with id {id} could be found");
            return Ok(language);
        }
    }
}
