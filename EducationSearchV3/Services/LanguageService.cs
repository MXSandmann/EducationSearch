using EducationSearchV3.Models;
using EducationSearchV3.Models.Dtos.Responses;
using EducationSearchV3.Repositories.Contracts;
using EducationSearchV3.Services.Contracts;

namespace EducationSearchV3.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly ILanguageRepository _languageRepository;
        public LanguageService(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public async Task<IEnumerable<GetLanguageDto>?> GetAll()
        {
            var languages = await _languageRepository.GetAllLanguages();
            if(languages is null) return null;
            var results = new List<GetLanguageDto>(languages.Count);
            foreach(var language in languages)
            {
                results.Add(
                    new GetLanguageDto
                    {
                        Name = language.Name.ToString(),
                        Countries = GetCountryNames(language),
                        EducationPrograms = GetProgramNames(language)
                    });
            }
            return results;
        }

        private IEnumerable<string> GetProgramNames(Language language)
        {
            if (language.EducationPrograms is null) return Enumerable.Empty<string>();
            var programNames = new List<string>(language.EducationPrograms.Count);
            foreach (var program in language.EducationPrograms)
            {
                programNames.Add(program.Name);
            }
            return programNames;
        }

        private IEnumerable<string> GetCountryNames(Language language)
        {
            if(language.Countries is null) return Enumerable.Empty<string>();
            var countryNames = new List<string>(language.Countries.Count);
            foreach (var country in language.Countries)
            {
                countryNames.Add(country.Name);
            }
            return countryNames;
        }

        public async Task<GetLanguageDto?> GetById(int id)
        {
            var language = await _languageRepository.GetLanguageById(id);
            if(language is null) return null;
            var result = new GetLanguageDto
            {
                Name = language.Name.ToString(),
                Countries = GetCountryNames(language),
                EducationPrograms = GetProgramNames(language)
            };
            return result;
        }
    }
}
