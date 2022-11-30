using EducationSearchV3.Models.Dtos.Requests;
using EducationSearchV3.Models.Dtos.Responses;

namespace EducationSearchV3.Services
{
    public interface IHighSchoolService
    {
        Task<IEnumerable<GetHighSchoolDto>?> Create(CreateUpdateHighSchoolDto dto);
        Task<IEnumerable<GetHighSchoolDto>?> GetAll();
        Task<GetHighSchoolDto?> GetById(int id);
        Task<GetHighSchoolDto?> Update(CreateUpdateHighSchoolDto dto);
        Task<IEnumerable<GetHighSchoolDto>?> Delete(int id);
    }
}
