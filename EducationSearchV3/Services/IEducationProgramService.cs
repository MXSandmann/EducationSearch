using EducationSearchV3.Models.Dtos.Requests;
using EducationSearchV3.Models.Dtos.Responses;

namespace EducationSearchV3.Services
{
    public interface IEducationProgramService
    {
        Task<IEnumerable<GetEPDto>?> Create(CreateUpdateEPDto dto);
        Task<IEnumerable<GetEPDto>?> GetAll();
        Task<GetEPDto?> GetById(int id);
        Task<GetEPDto?> Update(CreateUpdateEPDto dto);
        Task<IEnumerable<GetEPDto>?> Delete(int id);
    }
}
