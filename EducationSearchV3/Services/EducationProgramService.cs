using EducationSearchV3.Extensions;
using EducationSearchV3.Models;
using EducationSearchV3.Models.Dtos.Requests;
using EducationSearchV3.Models.Dtos.Responses;
using EducationSearchV3.Models.Enums;
using EducationSearchV3.Repositories.Contracts;
using EducationSearchV3.Services.Contracts;
using System.Diagnostics.CodeAnalysis;

namespace EducationSearchV3.Services
{
    public class EducationProgramService : IEducationProgramService
    {
        private readonly IEducationProgramRepository _educationProgramRepository;
        private readonly IHighSchoolRepository _highSchoolRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly ISubjectRepository _subjectRepository;
        public EducationProgramService(IEducationProgramRepository educationProgramRepository, IHighSchoolRepository highSchoolRepository, ILanguageRepository languageRepository, ISubjectRepository subjectRepository)
        {
            _educationProgramRepository = educationProgramRepository;
            _highSchoolRepository = highSchoolRepository;
            _languageRepository = languageRepository;
            _subjectRepository = subjectRepository;
        }

        public async Task<IEnumerable<GetEPDto>?> GetAll()
        {
            return await GetAllProgramsWithDependentsAsync();
        }

        private async Task<IEnumerable<GetEPDto>?> GetAllProgramsWithDependentsAsync()
        {
            var programs = await _educationProgramRepository.GetAllPrograms();
            if (programs is null) return null;
            var results = new List<GetEPDto>(programs.Count);
            foreach(var program in programs)
            {
                results.Add(
                    new GetEPDto
                    {
                        Id = program.Id,
                        Name = program.Name,
                        EducationForm = program.EducationForm.ToString(),
                        Requirements = program.Requirements,
                        HighSchool = program.HighSchool.Name,
                        StudyLevel = program.StudyLevel.ToString(),
                        Languages = GetLanguageNames(program),
                        Subjects = GetSubjectNames(program)
                    });
            }
            return results;
        }

        public async Task<GetEPDto?> GetById(int id)
        {
            return await GetOneProgramWithDependentsAsync(id);
        }

        private async Task<GetEPDto?> GetOneProgramWithDependentsAsync(int id)
        {
            var program = await _educationProgramRepository.GetProgramById(id);
            if (program is null) return null;
            var result = new GetEPDto
            {
                Id = program.Id,
                Name = program.Name,
                EducationForm = program.EducationForm.ToString(),
                Requirements = program.Requirements,
                HighSchool = program.HighSchool.Name,
                StudyLevel = program.StudyLevel.ToString(),
                Languages = GetLanguageNames(program),
                Subjects = GetSubjectNames(program)
            };
            return result;              
        }

        public async Task<IEnumerable<GetEPDto>?> Create(CreateEPDto dto)
        {
            var found = await _educationProgramRepository.HasProgramWithName(dto.Name);
            if (found) return null;
            var newProgram = new EducationProgram
            {
                Name = dto.Name,
                EducationForm = ParseToEntity<EducationsForms>(dto.EducationForm),
                Requirements = dto.Requirements,
                HighSchool = await FindHighSchoolForDBAsync(dto.HighSchoolId),
                StudyLevel = ParseToEntity<StudyLevels>(dto.StudyLevel),
                Languages = await FindLanguagesForDBAsync(dto.LanguageIds),
                Subjects = await FindSubjectsForDBAsync(dto.SubjectIds)
            };
            await _educationProgramRepository.AddProgram(newProgram);
            return await GetAllProgramsWithDependentsAsync();            
        }
                

        private async Task<HighSchool> FindHighSchoolForDBAsync(int id)
        {
            var highSchool = await _highSchoolRepository.GetHighSchoolById(id);
            if (highSchool is null)
                throw new ArgumentException($"The high school with id {id} does not exist");
            return highSchool;
        }

        private static T ParseToEntity<T>(int value) 
        {
            if (!Enum.IsDefined(typeof(T), value))
                throw new ArgumentOutOfRangeException($"The is no {nameof(T)} corresponding to {value}");
            return (T)Enum.ToObject(typeof(T), value);
        }

        public async Task<IEnumerable<GetEPDto>?> Delete(int id)
        {
            var foundProgram = await _educationProgramRepository.GetProgramById(id);
            if (foundProgram is null) return null;
            await _educationProgramRepository.RemoveProgram(foundProgram);
            return await GetAllProgramsWithDependentsAsync();
        }          

        public async Task<GetEPDto?> Update(UpdateEPDto dto)
        {            
            var epToUpdate = await _educationProgramRepository.GetProgramById(dto.Id);
            if(epToUpdate is null) return null;

            if(!string.IsNullOrWhiteSpace(dto.Name))
                epToUpdate.Name = dto.Name;
            if(dto.EducationForm is not null)
                epToUpdate.EducationForm = ParseToEntity<EducationsForms>(dto.EducationForm.Value);
            if(!string.IsNullOrWhiteSpace(dto.Requirements))
                epToUpdate.Requirements = dto.Requirements;
            if (dto.HighSchoolId is not null)
                epToUpdate.HighSchool = await FindHighSchoolForDBAsync(dto.HighSchoolId.Value);
            if (dto.StudyLevel is not null)
                epToUpdate.StudyLevel = ParseToEntity<StudyLevels>(dto.StudyLevel.Value);
            var newLanguages = await FindLanguagesForDBAsync(dto.LanguageIds);
            if(newLanguages.Any())
                epToUpdate.Languages.Replace(newLanguages);
            var newSubjects = await FindSubjectsForDBAsync(dto.SubjectIds);
            if (newSubjects.Any())
                epToUpdate.Subjects.Replace(newSubjects);

            await _educationProgramRepository.SaveChangesAsync();
            Console.WriteLine($"--> DEBUG: epToUpdate.Id : {epToUpdate.Id}");
            return await GetOneProgramWithDependentsAsync(epToUpdate.Id);
        }

        private static IEnumerable<string> GetLanguageNames(EducationProgram program)
        {
            if (program.Languages is null) return Enumerable.Empty<string>();
            var languageNames = new List<string>(program.Languages.Count);
            foreach (var language in program.Languages)
                languageNames.Add(language.Name.ToString());
            return languageNames;
        }

        private static IEnumerable<string> GetSubjectNames(EducationProgram program)
        {
            if (program.Subjects is null) return Enumerable.Empty<string>();
            var subjectNames = new List<string>(program.Subjects.Count);
            foreach (var subject in program.Subjects)
                subjectNames.Add(subject.Name.ToString());
            return subjectNames;
        }

        private async Task<ICollection<Language>> FindLanguagesForDBAsync(IEnumerable<int>? ids)
        {
            // Null check
            if (ids is null) return Array.Empty<Language>();

            // Create a list of languages, which will be added for a country
            List<Language> languages = new(ids.ToList().Count);

            // Find the existing languages in data context by the given from dto id
            foreach (var languageAsInt in ids)
            {
                var language = await _languageRepository.GetLanguageById(languageAsInt);
                if (language is null)
                    throw new ArgumentException($"The given language does not exist in the db: {languageAsInt}");
                languages.Add(language);
            }
            return languages;
        }

        private async Task<ICollection<Subject>> FindSubjectsForDBAsync(IEnumerable<int>? ids)
        {
            if (ids is null) return Array.Empty<Subject>();
            List<Subject> subjects = new(ids.ToList().Count);
            foreach (var subjectId in ids)
            {
                var subject = await _subjectRepository.GetSubjectById(subjectId);
                if (subject is null)
                    throw new ArgumentException($"The given subject does not exist in the db: {subjectId}");
                subjects.Add(subject);
            }
            return subjects;
        }
    }
}
