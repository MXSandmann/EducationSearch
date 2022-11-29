using EducationSearchV3.Models;

namespace EducationSearchV3.Repositories
{
    public interface ISubjectRepository
    {
        public Task<List<Subject>> GetAllSubjects();
        public Task<Subject?> GetSubjectById(int id);
        public Task<bool> HasSubjectWithName(string name);
        public Task AddSubject(Subject subject);
        public Task RemoveSubject(Subject subject);
        public Task SaveChangesAsync();
    }
}
