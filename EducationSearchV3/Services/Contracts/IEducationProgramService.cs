using EducationSearchV3.Models.Dtos.Requests;
using EducationSearchV3.Models.Dtos.Responses;

namespace EducationSearchV3.Services.Contracts
{
    public interface IEducationProgramService
    {
        Task<IEnumerable<GetEPDto>?> Create(CreateEPDto dto);
        Task<IEnumerable<GetEPDto>?> GetAll();
        Task<GetEPDto?> GetById(int id);
        Task<GetEPDto?> Update(UpdateEPDto dto);
        Task<IEnumerable<GetEPDto>?> Delete(int id);
    }
}
