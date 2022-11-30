using EducationSearchV3.Data;
using EducationSearchV3.Models;
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
        public async Task<Language?> GetLanguageById(int id)
        {
            return await _context.Languages.FirstOrDefaultAsync(l => (int)l.Name == id);
        }
    }
}
