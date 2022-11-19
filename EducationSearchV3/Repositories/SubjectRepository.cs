using EducationSearchV3.Data;
using EducationSearchV3.Models;
using EducationSearchV3.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using static System.Net.Mime.MediaTypeNames;

namespace EducationSearchV3.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly DataContext _context;

        public SubjectRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Subject>> GetAll()
        {
            return await _context.Subjects.Include(s => s.Programs).ToListAsync();
        }
        public async Task<Subject?> GetById(int id)
        {
           return await _context.Subjects.Include(s => s.Programs).FirstOrDefaultAsync(s => s.Id == id);            
        }

        public async Task<IEnumerable<Subject>?> Create(SubjectDto dto)
        {
            // Check first if the subject with the input name already exists
            var found = await _context.Subjects.AnyAsync(s => s.Name == dto.Name);

            if (found)
                return null;

            // Create new one
            var newSubject = new Subject { Name = dto.Name };
            await _context.Subjects.AddAsync(newSubject);
            await _context.SaveChangesAsync();
            return await _context.Subjects.Include(s => s.Programs).ToListAsync();
        }

        public async Task<IEnumerable<Subject>?> Delete(int id)
        {
            // Check first if the subject to delete exists
            var foundSubject = await _context.Subjects.FindAsync(id);

            if (foundSubject == null)
                return null;

            _context.Subjects.Remove(foundSubject);
            await _context.SaveChangesAsync();
            return await _context.Subjects.Include(s => s.Programs).ToListAsync();
        }



        public async Task<Subject?> Update(SubjectDto dto)
        {
            // Check first if the subject to update exists
            var subjectToUpdate = await _context.Subjects.FirstOrDefaultAsync(s => s.Id == dto.Id);

            if (subjectToUpdate == null)
                return null;

            subjectToUpdate.Name = dto.Name;
            await _context.SaveChangesAsync();
            return subjectToUpdate;
        }
    }
}
