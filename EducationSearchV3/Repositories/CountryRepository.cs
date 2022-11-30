using EducationSearchV3.Data;
using EducationSearchV3.Models;
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

        public async Task AddCountry(Country country)
        {
            await _context.Countries.AddAsync(country);
            await SaveChangesAsync();
        }

        public async Task<List<Country>> GetAllCountries()
        {
            return await _context.Countries
                .Include(c => c.Languages)
                .Include(c => c.HighSchools)
                .ToListAsync();
        }

        public async Task<Country?> GetCountryById(int id)
        {
            return await _context.Countries
                .Include(c => c.Languages)
                .Include(c => c.HighSchools)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Country?> GetCountryByName(string name)
        {
            return await _context.Countries
                .Include(c => c.Languages)
                .Include(c => c.HighSchools)
                .SingleOrDefaultAsync(c => c.Name == name);
        }

        public async Task<bool> HasCountryWithName(string name)
        {
            return await _context.Countries.AnyAsync(c => c.Name == name);
        }

        public async Task RemoveCountry(Country country)
        {
            _context.Countries.Remove(country);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
