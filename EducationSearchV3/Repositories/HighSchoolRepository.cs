using EducationSearchV3.Data;
using EducationSearchV3.Models;
using Microsoft.EntityFrameworkCore;

namespace EducationSearchV3.Repositories
{
    public class HighSchoolRepository : IHighSchoolRepository
    {
        private readonly DataContext _context;

        public HighSchoolRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddHighSchool(HighSchool hs)
        {
            await _context.HighSchools.AddAsync(hs);
            await SaveChangesAsync();
        }

        public async Task<List<HighSchool>> GetAllHighSchools()
        {
            return await _context.HighSchools
                .Include(h => h.Country)
                .Include(h => h.Programs)
                .ToListAsync();
        }

        public async Task<HighSchool?> GetHighSchoolById(int id)
        {
            return await _context.HighSchools
                .Include(h => h.Country)
                .Include(h => h.Programs)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<bool> HasHighSchoolWithName(string name)
        {
            return await _context.HighSchools.AnyAsync(h => h.Name == name);
        }

        public async Task RemoveHighSchool(HighSchool hs)
        {
            _context.HighSchools.Remove(hs);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
