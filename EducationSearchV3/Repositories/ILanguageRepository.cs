using EducationSearchV3.Models;

namespace EducationSearchV3.Repositories
{
    public interface ILanguageRepository
    {
        public Task<Language?> GetLanguageById(int id);
    }
}
