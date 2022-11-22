using EducationSearchV3.Data;
using EducationSearchV3.Models;
using EducationSearchV3.Models.Dtos;
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

        public async Task<IEnumerable<HighSchool>> GetAll()
        {
            return await _context.HighSchools.Include(h => h.Programs).ToListAsync();
        }

        public async Task<HighSchool?> GetById(int id)
        {
            return await _context.HighSchools.Include(h => h.Programs).FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<IEnumerable<HighSchool>?> Create(HighSchoolDto dto)
        {
            // Check first if the object with the input name already exists
            var found = await _context.HighSchools.AnyAsync(s => s.Name == dto.Name);

            if (found)
                return null;

            // Create new one
            var newHighSchool = new HighSchool
            {
                Name = dto.Name,
                Programs = await GetPrograms(dto)
            };
            await _context.HighSchools.AddAsync(newHighSchool);
            await _context.SaveChangesAsync();
            return await _context.HighSchools
                .Include(s => s.Programs)
                .ToListAsync();
        }

        public async Task<IEnumerable<HighSchool>?> Delete(int id)
        {
            // Check first if the object to delete exists
            var foundHighSchool = await _context.HighSchools.FindAsync(id);

            if (foundHighSchool == null)
                return null;

            _context.HighSchools.Remove(foundHighSchool);
            await _context.SaveChangesAsync();
            return await _context.HighSchools
                .Include(h => h.Programs)
                .ToListAsync();
        }               

        public async Task<HighSchool?> Update(HighSchoolDto dto)
        {
            // Check first if the object to update exists
            var highSchoolToUpdate = await _context.HighSchools
                .FirstOrDefaultAsync(h => h.Id == dto.Id);

            if (highSchoolToUpdate == null)
                return null;

            highSchoolToUpdate.Name = dto.Name;
            highSchoolToUpdate.Programs = await GetPrograms(dto);
            await _context.SaveChangesAsync();
            return highSchoolToUpdate;
        }

        private async Task<IEnumerable<EducationProgram>> GetPrograms(HighSchoolDto dto)
        {
            if (dto.ProgramIds == null) return Enumerable.Empty<EducationProgram>();

            // Create a list of programs, which will be added for a high school
            List<EducationProgram> programs = new(dto.ProgramIds.Count);

            // Find the existing languages in data context by the given from dto id
            foreach (var programId in dto.ProgramIds)
            {
                var program = await _context.EducationPrograms
                    .FirstOrDefaultAsync(p => p.Id == programId);
                if (program == null)
                    throw new ArgumentException($"The given education program does not exist in the databese: {programId}");

                programs.Add(program);
            }
            return programs;
        }
    }
}
