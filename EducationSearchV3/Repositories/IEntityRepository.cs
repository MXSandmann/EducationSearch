using EducationSearchV3.Models.Dtos;
using EducationSearchV3.Models;

namespace EducationSearchV3.Repositories
{
    public interface IEntityRepository<TEntity, TDto> : IBaseRepository
    {
        Task<IEnumerable<TEntity>?> Create(TDto dto);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity?> GetById(int id);
        Task<TEntity?> Update(TDto dto);
        Task<IEnumerable<TEntity>?> Delete(int id);
    }
}
