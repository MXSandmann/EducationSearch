using EducationSearchV3.Models.Dtos;
using EducationSearchV3.Models;

namespace EducationSearchV3.Repositories
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>?> Create(CountryDto dto);
        Task<IEnumerable<Country>> GetAll();
        Task<Country?> GetById(int id);
        Task<Country?> Update(CountryDto dto);
        Task<IEnumerable<Country>?> Delete(int id);
    }
}
