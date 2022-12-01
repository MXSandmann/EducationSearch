using EducationSearchV3.Models;

namespace EducationSearchV3.Repositories
{
    public interface IEducationProgramRepository
    {
        public Task<EducationProgram?> GetProgramById(int id);
        public Task<List<EducationProgram>> GetAllPrograms();
        public Task<bool> HasProgramWithName(string name);
        public Task AddProgram(EducationProgram program);
        public Task RemoveProgram(EducationProgram program);
        public Task SaveChangesAsync();
    }
}
