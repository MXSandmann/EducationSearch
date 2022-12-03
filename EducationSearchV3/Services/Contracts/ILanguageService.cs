using EducationSearchV3.Models;
using EducationSearchV3.Models.Dtos.Responses;

namespace EducationSearchV3.Services.Contracts
{
    public interface ILanguageService
    {
        Task<IEnumerable<GetLanguageDto>?> GetAll();
        Task<GetLanguageDto?> GetById(int id);
    }
}
