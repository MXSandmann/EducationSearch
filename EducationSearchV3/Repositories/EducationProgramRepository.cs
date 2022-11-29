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

        public async Task<EducationProgram?> GetSubjectById(int id)
        {
            return await _context.EducationPrograms.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
