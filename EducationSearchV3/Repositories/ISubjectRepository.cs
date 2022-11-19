using EducationSearchV3.Models;
using EducationSearchV3.Models.Dtos;
using System.Runtime.InteropServices;

namespace EducationSearchV3.Repositories
{
    public interface ISubjectRepository
    {
        Task<IEnumerable<Subject>?> Create(SubjectDto dto);
        Task<IEnumerable<Subject>> GetAll();
        Task<Subject?> GetById(int id);
        Task<Subject?> Update(SubjectDto dto);
        Task<IEnumerable<Subject>?> Delete(int id);

    }
}
