using EducationSearchV3.Models;

namespace EducationSearchV3.Repositories.Contracts
{
    public interface ILanguageRepository
    {
        public Task<Language?> GetLanguageById(int id);
        public Task<List<Language>> GetAllLanguages();
    }
}
