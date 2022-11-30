using EducationSearchV3.Models;
using EducationSearchV3.Models.Dtos.Requests;
using EducationSearchV3.Models.Dtos.Responses;
using EducationSearchV3.Repositories;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

namespace EducationSearchV3.Services
{
    public class HighSchoolService : IHighSchoolService
    {
        private readonly IHighSchoolRepository _highSchoolRepository;
        private readonly Repositories.ICountryRepository _countryRepository;
        private readonly IEducationProgramRepository _educationProgramRepository;
        public HighSchoolService(IHighSchoolRepository highSchoolRepository, Repositories.ICountryRepository countryRepository, IEducationProgramRepository educationProgramRepository)
        {
            _highSchoolRepository = highSchoolRepository;
            _countryRepository = countryRepository;
            _educationProgramRepository = educationProgramRepository;
        }

        public async Task<IEnumerable<GetHighSchoolDto>?> GetAll()
        {
            return await GetAllHighSchoolsWithDependentsAsync();
        }

        private async Task<IEnumerable<GetHighSchoolDto>?> GetAllHighSchoolsWithDependentsAsync()
        {
            var schools = await _highSchoolRepository.GetAllHighSchools();
            if (schools is null) return null;
            var results = new List<GetHighSchoolDto>(schools.Count);
            foreach (var school in schools)
            {
                results.Add(
                    new GetHighSchoolDto
                    {
                        Id = school.Id,
                        Name = school.Name,
                        Country = school.Country.Name,
                        Programs = GetProgramNames(school)
                    });
            }
            return results;
        }

        private static IEnumerable<string> GetProgramNames(HighSchool school)
        {
            if (school.Programs is null) return Enumerable.Empty<string>();
            var programNames = new List<string>(school.Programs.Count);
            foreach (var program in school.Programs)
                programNames.Add(program.Name);
            return programNames;
        }

        public async Task<GetHighSchoolDto?> GetById(int id)
        {
            return await GetOneHighSchoolWithDependentsAsync(id);
        }

        private async Task<GetHighSchoolDto?> GetOneHighSchoolWithDependentsAsync(int id)
        {
            var school = await _highSchoolRepository.GetHighSchoolById(id);
            if (school is null) return null;
            var result = new GetHighSchoolDto
            {
                Id = school.Id,
                Name = school.Name,
                Country = school.Country.Name,
                Programs = GetProgramNames(school)
            };
            return result;
        }

        public async Task<IEnumerable<GetHighSchoolDto>?> Create(CreateUpdateHighSchoolDto dto)
        {
            // Check first if the object with the input name already exists
            var found = await _highSchoolRepository.HasHighSchoolWithName(dto.Name);
            if (found) return null;

            // Create new one
            var newHighSchool = new HighSchool
            {
                Name = dto.Name,
                Country = await GetCountry(dto.Country),
                Programs = await GetPrograms(dto)
            };
            await _highSchoolRepository.AddHighSchool(newHighSchool);
            return await GetAllHighSchoolsWithDependentsAsync();
        }

        private async Task<Country> GetCountry(string country)
        {
            var _country = await _countryRepository.GetCountryByName(country);
            if (_country is null)
                throw new ArgumentNullException($"Country {country} could not been found");
            return _country;
        }

        public async Task<IEnumerable<GetHighSchoolDto>?> Delete(int id)
        {
            // Check first if the object to delete exists
            var foundHighSchool = await _highSchoolRepository.GetHighSchoolById(id);
            if (foundHighSchool is null) return null;
            await _highSchoolRepository.RemoveHighSchool(foundHighSchool);
            return await GetAllHighSchoolsWithDependentsAsync();
        }

        public async Task<GetHighSchoolDto?> Update(CreateUpdateHighSchoolDto dto)
        {
            if (dto.Id is null) return null;

            // Check first if the object to update exists
            var schoolToUpdate = await _highSchoolRepository.GetHighSchoolById(dto.Id.Value);

            if (schoolToUpdate == null) return null;

            schoolToUpdate.Name = dto.Name;
            schoolToUpdate.Country = await GetCountry(dto.Country);
            schoolToUpdate.Programs = await GetPrograms(dto);
            await _highSchoolRepository.SaveChangesAsync();
            return await GetOneHighSchoolWithDependentsAsync(dto.Id.Value);
        }

        private async Task<ICollection<EducationProgram>> GetPrograms(CreateUpdateHighSchoolDto dto)
        {
            if (dto.ProgramIds == null) return Array.Empty<EducationProgram>();

            // Create a list of programs, which will be added for a high school
            List<EducationProgram> programs = new(dto.ProgramIds.Count);

            // Find the existing languages in data context by the given from dto id
            foreach (var programId in dto.ProgramIds)
            {
                var program = await _educationProgramRepository.GetProgramById(programId);
                if (program == null)
                    throw new ArgumentException($"The given education program does not exist in the databese: {programId}");

                programs.Add(program);
            }
            return programs;
        }
    }
}
