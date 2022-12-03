using EducationSearchV3.Data;
using EducationSearchV3.Models;
using EducationSearchV3.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EducationSearchV3.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly DataContext _context;
        public LanguageRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Language>> GetAllLanguages()
        {
            return await _context.Languages
                .Include(l => l.Countries)
                .Include(l => l.EducationPrograms)
                .ToListAsync();
        }

        public async Task<Language?> GetLanguageById(int id)
        {
            return await _context.Languages
                .Include(l => l.Countries)
                .Include(l => l.EducationPrograms)
                .FirstOrDefaultAsync(l => (int)l.Name == id);
        }
    }
}
