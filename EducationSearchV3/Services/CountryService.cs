using EducationSearchV3.Extensions;
using EducationSearchV3.Models;
using EducationSearchV3.Models.Dtos.Requests;
using EducationSearchV3.Models.Dtos.Responses;
using EducationSearchV3.Repositories.Contracts;
using EducationSearchV3.Services.Contracts;

namespace EducationSearchV3.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly IHighSchoolRepository _highSchoolRepository;

        public CountryService(ICountryRepository countryRepository, ILanguageRepository languageRepository, IHighSchoolRepository highSchoolRepository)
        {
            _countryRepository = countryRepository;
            _languageRepository = languageRepository;
            _highSchoolRepository = highSchoolRepository;
        }
        public async Task<IEnumerable<GetCountryDto>?> GetAll()
        {
            return await GetAllCountriesWithDependendentsAsync();
        }

        private async Task<IEnumerable<GetCountryDto>?> GetAllCountriesWithDependendentsAsync()
        {
            var countries = await _countryRepository.GetAllCountries();
            if (countries is null) return null;
            var results = new List<GetCountryDto>(countries.Count);
            foreach (var country in countries)
            {
                results.Add(
                    new GetCountryDto
                    {
                        Id = country.Id,
                        Name = country.Name,
                        Languages = GetLanguageNames(country),
                        HighSchools = GetHighSchoolNames(country)
                    });
            }
            return results;
        }

        public async Task<GetCountryDto?> GetById(int id)
        {
            return await GetOneCountryWithDependentsAsync(id);
        }

        private async Task<GetCountryDto?> GetOneCountryWithDependentsAsync(int id)
        {
            var country = await _countryRepository.GetCountryById(id);
            if (country is null) return null;
            var result = new GetCountryDto
            {
                Id = country.Id,
                Name = country.Name,
                Languages = GetLanguageNames(country),
                HighSchools = GetHighSchoolNames(country)
            };
            return result;
        }

        public async Task<IEnumerable<GetCountryDto>?> Create(CreateUpdateCountryDto dto)
        {
            // Check first if the object with the input name already exists
            var found = await _countryRepository.HasCountryWithName(dto.Name);

            if (found) return null;

            // Create new one
            var newCountry = new Country
            {
                Name = dto.Name,
                Languages = await FindLanguagesForDBAsync(dto),
                HighSchools = await FindHighSchoolsForDBAsync(dto)
            };
            await _countryRepository.AddCountry(newCountry);            
            return await GetAllCountriesWithDependendentsAsync();
        }

        public async Task<IEnumerable<GetCountryDto>?> Delete(int id)
        {
            // Check first if the object to delete exists
            var foundCountry = await _countryRepository.GetCountryById(id);
            if (foundCountry is null) return null;
            await _countryRepository.RemoveCountry(foundCountry);            
            return await GetAllCountriesWithDependendentsAsync();
        }

        public async Task<GetCountryDto?> Update(CreateUpdateCountryDto dto)
        {
            // Check for id
            if (dto.Id is null) return null;

            // Check first if the object to update exists
            var countryToUpdate = await _countryRepository.GetCountryById(dto.Id.Value);

            if (countryToUpdate is null) return null;

            countryToUpdate.Name = dto.Name;

            var newLanguages = await FindLanguagesForDBAsync(dto);
            if (newLanguages.Any())
                countryToUpdate.Languages.Replace(newLanguages);

            var newHSs = await FindHighSchoolsForDBAsync(dto);
            if (newHSs.Any())
                countryToUpdate.HighSchools.Replace(newHSs);

            await _countryRepository.SaveChangesAsync();
            return await GetOneCountryWithDependentsAsync(countryToUpdate.Id);
        }

        private async Task<ICollection<Language>> FindLanguagesForDBAsync(CreateUpdateCountryDto dto)
        {
            // Null check
            if (dto.LanguageIds is null) return Array.Empty<Language>();

            // Create a list of languages, which will be added for a country
            List<Language> languages = new(dto.LanguageIds.ToList().Count);

            // Find the existing languages in data context by the given from dto id
            foreach (var languageAsInt in dto.LanguageIds)
            {
                var language = await _languageRepository.GetLanguageById(languageAsInt);
                if (language is null)
                    throw new ArgumentException($"The given language does not exist in the db: {languageAsInt}");

                languages.Add(language);
            }
            return languages;
        }

        private async Task<ICollection<HighSchool>> FindHighSchoolsForDBAsync(CreateUpdateCountryDto dto)
        {
            if (dto.HighSchoolIds is null) return Array.Empty<HighSchool>();

            List<HighSchool> hss = new(dto.HighSchoolIds.ToList().Count);
            foreach (var hsId in dto.HighSchoolIds)
            {
                var highSchool = await _highSchoolRepository.GetHighSchoolById(hsId);
                if (highSchool is null)
                    throw new ArgumentException($"The high school with id {hsId} does not exist in the DB");

                hss.Add(highSchool);
            }
            return hss;
        }

        private static IEnumerable<string> GetLanguageNames(Country country)
        {
            if (country.Languages is null) return Enumerable.Empty<string>();
            var languageNames = new List<string>(country.Languages.Count);
            foreach (var language in country.Languages)
                languageNames.Add(language.Name.ToString());
            return languageNames;
        }

        private static IEnumerable<string> GetHighSchoolNames(Country country)
        {
            if (country.HighSchools is null) return Enumerable.Empty<string>();
            var highSchoolNames = new List<string>(country.HighSchools.Count);
            foreach (var hs in country.HighSchools)
                highSchoolNames.Add(hs.Name);
            return highSchoolNames;
        }
    }
}
