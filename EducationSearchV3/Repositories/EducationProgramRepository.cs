using EducationSearchV3.Data;
using EducationSearchV3.Models;
using Microsoft.EntityFrameworkCore;

namespace EducationSearchV3.Repositories
{
    public class EducationProgramRepository : IEducationProgramRepository
    {
        private readonly DataContext _context;
        public EducationProgramRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public async Task AddProgram(EducationProgram program)
        {
            await _context.EducationPrograms.AddAsync(program);
            await SaveChangesAsync();
        }

        public async Task<List<EducationProgram>> GetAllPrograms()
        {
            return await _context.EducationPrograms
                .Include(e => e.Languages)
                .Include(e => e.Subjects)
                .Include(e => e.HighSchool)
                .ToListAsync();
        }

        public async Task<EducationProgram?> GetProgramById(int id)
        {
            return await _context.EducationPrograms.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> HasProgramWithName(string name)
        {
            return await _context.EducationPrograms.AnyAsync(e => e.Name == name);
        }

        public async Task RemoveProgram(EducationProgram program)
        {
            _context.EducationPrograms.Remove(program);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
