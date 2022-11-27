using EducationSearchV3.Models.Dtos.Requests;
using EducationSearchV3.Models.Dtos.Responses;

namespace EducationSearchV3.Repositories
{
    public interface ISubjectRepository
    {
        Task<IEnumerable<GetSubjectDto>?> Create(CreateUpdateSubjectDto dto);
        Task<IEnumerable<GetSubjectDto>?> GetAll();
        Task<GetSubjectDto?> GetById(int id);
        Task<GetSubjectDto?> Update(CreateUpdateSubjectDto dto);
        Task<IEnumerable<GetSubjectDto>?> Delete(int id);
    }
}
