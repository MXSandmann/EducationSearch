using EducationSearchV3.Data;
using EducationSearchV3.Models;
using EducationSearchV3.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EducationSearchV3.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly DataContext _context;

        public SubjectRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Subject>> GetAllSubjects()
        {
            return await _context.Subjects.Include(s => s.Programs).ToListAsync();
        }

        public async Task<Subject?> GetSubjectById(int id)
        {
            return await _context.Subjects
                .Include(s => s.Programs)             
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> HasSubjectWithName(string name)
        {
            return await _context.Subjects.AnyAsync(s => s.Name == name);
        }

        public async Task AddSubject(Subject subject)
        {
            await _context.Subjects.AddAsync(subject);
            await SaveChangesAsync();
        }
              
        public async Task RemoveSubject(Subject subject)
        {
            _context.Subjects.Remove(subject);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
