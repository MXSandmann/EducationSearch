using EducationSearchV3.Data;
using EducationSearchV3.Models;
using EducationSearchV3.Models.Dtos;
using EducationSearchV3.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;

namespace EducationSearchV3.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;

        public CountryRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Country>> GetAll()
        {
            return await _context.Countries
                .Include(c => c.Languages)
                .Include(c => c.HighSchools)
                .ToListAsync();
        }

        public async Task<Country?> GetById(int id)
        {
            return await _context.Countries
                .Include(c => c.Languages)
                .Include(c => c.HighSchools)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Country>?> Create(CountryDto dto)
        {
            // Check first if the object with the input name already exists
            var found = await _context.Countries.AnyAsync(s => s.Name == dto.Name);
            
            if (found)
                return null;

            // Create a list of languages, which will be added for a new country
            List<Language> languages = new(dto.Languages.Count);

            // Find the existing languages in data context by the given from dto id
            foreach (var languageAsInt in dto.Languages)
            {
                var language = _context.Languages.FirstOrDefault(l => (int)l.Name == languageAsInt);
                if (language == null)
                    return null;

                languages.Add(language);
            }
                        
            // Create new one
            var newCountry = new Country {
                Name = dto.Name,
                Languages = languages                
            };

            await _context.Countries.AddAsync(newCountry);
            await _context.SaveChangesAsync();
            return await _context.Countries
                .Include(c => c.Languages)
                .Include(c => c.HighSchools)
                .ToListAsync();

        }

        public async Task<IEnumerable<Country>?> Delete(int id)
        {
            // Check first if the object to delete exists
            var foundCountry = await _context.Countries.FindAsync(id);

            if (foundCountry == null)
                return null;

            _context.Countries.Remove(foundCountry);
            await _context.SaveChangesAsync();
            return await _context.Countries
               .Include(c => c.Languages)
               .Include(c => c.HighSchools)
               .ToListAsync();
        }        

        public async Task<Country?> Update(CountryDto dto)
        {
            // Check first if the object to update exists
            var countryToUpdate = await _context.Countries                
                .FirstOrDefaultAsync(s => s.Id == dto.Id);

            if (countryToUpdate == null)
                return null;

            countryToUpdate.Name = dto.Name;
            await _context.SaveChangesAsync();
            return countryToUpdate;
        }
    }
}
