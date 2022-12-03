using EducationSearchV3.Extensions;
using EducationSearchV3.Models;
using EducationSearchV3.Models.Dtos.Requests;
using EducationSearchV3.Models.Dtos.Responses;
using EducationSearchV3.Repositories.Contracts;
using EducationSearchV3.Services.Contracts;

namespace EducationSearchV3.Services
{
    public class SubjectService : ISubjectService
    {        
        private readonly ISubjectRepository _subjectRepository;
        private readonly IEducationProgramRepository _educationProgramRepository;

        public SubjectService(ISubjectRepository subjectRepository, IEducationProgramRepository educationProgramRepository)
        {            
            _subjectRepository = subjectRepository;
            _educationProgramRepository = educationProgramRepository;
        }
        public async Task<IEnumerable<GetSubjectDto>?> GetAll()
        {
            return await GetAllSubjectsWithDependentsAsync();
        }

        private async Task<IEnumerable<GetSubjectDto>?> GetAllSubjectsWithDependentsAsync()
        {
            var subjects = await _subjectRepository.GetAllSubjects();
            if (subjects is null) return null;
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
            var subject = await _subjectRepository.GetSubjectById(id);
            if (subject is null) return null;
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
            var found = await _subjectRepository.HasSubjectWithName(dto.Name);
            if (found) return null;

            // Create new one
            var newSubject = new Subject
            {
                Name = dto.Name,
                Programs = await FindProgramsForDBAsync(dto)
            };
            await _subjectRepository.AddSubject(newSubject);            
            return await GetAllSubjectsWithDependentsAsync();
        }

        public async Task<IEnumerable<GetSubjectDto>?> Delete(int id)
        {
            // Check first if the object to delete exists
            var foundSubject = await _subjectRepository.GetSubjectById(id);
            if (foundSubject is null) return null;
            await _subjectRepository.RemoveSubject(foundSubject);            
            return await GetAllSubjectsWithDependentsAsync();
        }

        public async Task<GetSubjectDto?> Update(CreateUpdateSubjectDto dto)
        {
            if (dto.Id is null) return null;

            // Check first if the subject to update exists
            var subjectToUpdate = await _subjectRepository.GetSubjectById(dto.Id.Value);

            if (subjectToUpdate is null) return null;

            subjectToUpdate.Name = dto.Name;

            var newPrograms = await FindProgramsForDBAsync(dto);
            if (newPrograms.Any())
                subjectToUpdate.Programs!.Replace(newPrograms);

            await _subjectRepository.SaveChangesAsync();
            return await GetOneSubjectWithDependentsAsync(dto.Id.Value);
        }

        private async Task<ICollection<EducationProgram>> FindProgramsForDBAsync(CreateUpdateSubjectDto dto)
        {
            if (dto.ProgramIds is null) return Array.Empty<EducationProgram>();

            List<EducationProgram> programs = new(dto.ProgramIds.ToList().Count);

            foreach (var programId in dto.ProgramIds)
            {
                var program = await _educationProgramRepository.GetProgramById(programId);
                if (program is null)
                    throw new ArgumentException($"The given educations program with id {programId} does not exist in db");

                programs.Add(program);
            }
            return programs;
        }
    }
}
