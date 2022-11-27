using EducationSearchV3.Data;
using EducationSearchV3.Extensions;
using EducationSearchV3.Models;
using EducationSearchV3.Models.Dtos.Requests;
using EducationSearchV3.Models.Dtos.Responses;
using Microsoft.EntityFrameworkCore;

namespace EducationSearchV3.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;

        public CountryRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<GetCountryDto>?> GetAll()
        {
            return await GetAllCountriesWithDependendentsAsync();
        }

        private async Task<IEnumerable<GetCountryDto>?> GetAllCountriesWithDependendentsAsync()
        {
            var countries = await _context.Countries
                            .Include(c => c.Languages)
                            .Include(c => c.HighSchools)
                            .ToListAsync();

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
            var country = await _context.Countries
                            .Include(c => c.Languages)
                            .Include(c => c.HighSchools)
                            .FirstOrDefaultAsync(c => c.Id == id);

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
            var found = await _context.Countries.AnyAsync(s => s.Name == dto.Name);

            if (found) return null;

            // Create new one
            var newCountry = new Country
            {
                Name = dto.Name,
                Languages = await FindLanguagesForDBAsync(dto),
                HighSchools = await FindHighSchoolsForDBAsync(dto)
            };
            await _context.Countries.AddAsync(newCountry);
            await _context.SaveChangesAsync();
            return await GetAllCountriesWithDependendentsAsync();
        }

        public async Task<IEnumerable<GetCountryDto>?> Delete(int id)
        {
            // Check first if the object to delete exists
            var foundCountry = await _context.Countries.FindAsync(id);

            if (foundCountry is null)
                return null;

            _context.Countries.Remove(foundCountry);
            await _context.SaveChangesAsync();
            return await GetAllCountriesWithDependendentsAsync();
        }        

        public async Task<GetCountryDto?> Update(CreateUpdateCountryDto dto)
        {
            // Check for id
            if (dto.Id is null) return null;

            // Check first if the object to update exists
            var countryToUpdate = await _context.Countries
                .Include(c => c.Languages)
                .Include(c => c.HighSchools)
                .FirstOrDefaultAsync(s => s.Id == dto.Id);

            if (countryToUpdate is null) return null;

            countryToUpdate.Name = dto.Name;

            var newLanguages = await FindLanguagesForDBAsync(dto);
            if(newLanguages.Any())            
                countryToUpdate.Languages.Replace(newLanguages);                
            
            var newHSs = await FindHighSchoolsForDBAsync(dto);
            if (newHSs.Any())
                countryToUpdate.HighSchools.Replace(newHSs);                
        
            await _context.SaveChangesAsync();
            return await GetOneCountryWithDependentsAsync(dto.Id.Value);
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
                var language = await _context.Languages.FirstOrDefaultAsync(l => (int)l.Name == languageAsInt);
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
                var highSchool = await _context.HighSchools.FirstOrDefaultAsync(h => h.Id == hsId);
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
