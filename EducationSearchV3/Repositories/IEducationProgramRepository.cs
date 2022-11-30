using EducationSearchV3.Models;

namespace EducationSearchV3.Repositories
{
    public interface IEducationProgramRepository
    {
        public Task<EducationProgram?> GetProgramById(int id);
    }
}
