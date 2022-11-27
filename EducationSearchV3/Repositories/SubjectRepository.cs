using EducationSearchV3.Data;
using EducationSearchV3.Extensions;
using EducationSearchV3.Models;
using EducationSearchV3.Models.Dtos;
using EducationSearchV3.Models.Dtos.Requests;
using EducationSearchV3.Models.Dtos.Responses;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace EducationSearchV3.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly DataContext _context;

        public SubjectRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<GetSubjectDto>?> GetAll()
        {
            return await GetAllSubjectsWithDependentsAsync();
        }

        private async Task<IEnumerable<GetSubjectDto>?> GetAllSubjectsWithDependentsAsync()
        {
            var subjects = await _context.Subjects.Include(s => s.Programs).ToListAsync();
            if(subjects is null) return null;
            var results = new List<GetSubjectDto>(subjects.Count);
            foreach (var subject in subjects)
            {
                results.Add(
                    new GetSubjectDto
                    {
                        Id = subject.Id,
                        Name = subject.Name,
                        Programs = GetProgramNames(subject)
                    });
            }
            return results;
        }

        private static IEnumerable<string> GetProgramNames(Subject subject)
        {
            if (subject.Programs is null) return Enumerable.Empty<string>();
            var programNames = new List<string>(subject.Programs.Count);
            foreach (var program in subject.Programs)
                programNames.Add(program.Name);
            return programNames;
        }

        public async Task<GetSubjectDto?> GetById(int id)
        {
            return await GetOneSubjectWithDependentsAsync(id);
        }

        private async Task<GetSubjectDto?> GetOneSubjectWithDependentsAsync(int id)
        {
            var subject = await _context.Subjects
                .Include(s => s.Programs)
                .FirstOrDefaultAsync(s => s.Id == id);

            if(subject is null) return null;

            var result = new GetSubjectDto
            {
                Id = subject.Id,
                Name = subject.Name,
                Programs = GetProgramNames(subject)
            };
            return result;
        }

        public async Task<IEnumerable<GetSubjectDto>?> Create(CreateUpdateSubjectDto dto)
        {            
            var found = await _context.Subjects.AnyAsync(s => s.Name == dto.Name);

            if (found) return null;

            // Create new one
            var newSubject = new Subject
            {
                Name = dto.Name,
                Programs = await FindProgramsForDBAsync(dto)
            };
            await _context.Subjects.AddAsync(newSubject);
            await _context.SaveChangesAsync();
            return await GetAllSubjectsWithDependentsAsync();
        }

        public async Task<IEnumerable<GetSubjectDto>?> Delete(int id)
        {
            // Check first if the object to delete exists
            var foundSubject = await _context.Subjects.FindAsync(id);

            if (foundSubject is null) return null;

            _context.Subjects.Remove(foundSubject);
            await _context.SaveChangesAsync();
            return await GetAllSubjectsWithDependentsAsync();
        }

        public async Task<GetSubjectDto?> Update(CreateUpdateSubjectDto dto)
        {
            if (dto.Id is null) return null;

            // Check first if the subject to update exists
            var subjectToUpdate = await _context.Subjects
                .Include(s => s.Programs)
                .FirstOrDefaultAsync(s => s.Id == dto.Id);

            if (subjectToUpdate is null) return null;

            subjectToUpdate.Name = dto.Name;

            var newPrograms = await FindProgramsForDBAsync(dto);
            if (newPrograms.Any())
                subjectToUpdate.Programs!.Replace(newPrograms);

            await _context.SaveChangesAsync();
            return await GetOneSubjectWithDependentsAsync(dto.Id.Value);
        }

        private async Task<ICollection<EducationProgram>> FindProgramsForDBAsync(CreateUpdateSubjectDto dto)
        {
            if (dto.ProgramIds is null) return Array.Empty<EducationProgram>();

            List<EducationProgram> programs = new(dto.ProgramIds.ToList().Count);

            foreach (var programId in dto.ProgramIds)
            {
                var program = await _context.EducationPrograms
                    .FirstOrDefaultAsync(e => e.Id == programId);
                if (program is null)
                    throw new ArgumentException($"The given educations program with id {programId} does not exist in db");

                programs.Add(program);
            }
            return programs;
        }
    }
}
