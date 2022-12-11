using EducationSearchV3.Models.Dtos.Requests;
using EducationSearchV3.Models.Dtos.Responses;

namespace EducationSearchV3.Services.Contracts
{
    public interface ICountryService
    {
        Task<IEnumerable<GetCountryDto>?> Create(CreateCountryDto dto);
        Task<IEnumerable<GetCountryDto>?> GetAll();
        Task<GetCountryDto?> GetById(int id);
        Task<GetCountryDto?> Update(UpdateCountryDto dto);
        Task<IEnumerable<GetCountryDto>?> Delete(int id);
    }
}
