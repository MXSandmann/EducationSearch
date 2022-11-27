using EducationSearchV3.Models.Dtos.Requests;
using EducationSearchV3.Models.Dtos.Responses;

namespace EducationSearchV3.Repositories
{
    public interface ICountryRepository 
    {
        Task<IEnumerable<GetCountryDto>?> Create(CreateUpdateCountryDto dto);
        Task<IEnumerable<GetCountryDto>?> GetAll();
        Task<GetCountryDto?> GetById(int id);
        Task<GetCountryDto?> Update(CreateUpdateCountryDto dto);
        Task<IEnumerable<GetCountryDto>?> Delete(int id);
    }
}
