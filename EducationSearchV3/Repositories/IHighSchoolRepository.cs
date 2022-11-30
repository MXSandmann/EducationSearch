using EducationSearchV3.Models;

namespace EducationSearchV3.Repositories
{
    public interface IHighSchoolRepository
    {
        public Task<List<HighSchool>> GetAllHighSchools();
        public Task<HighSchool?> GetHighSchoolById(int id);
        public Task<bool> HasHighSchoolWithName(string name);
        public Task AddHighSchool(HighSchool hs);
        public Task RemoveHighSchool(HighSchool hs);
        public Task SaveChangesAsync();
    }
}
