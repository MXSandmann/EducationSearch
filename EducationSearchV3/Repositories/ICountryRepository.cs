using EducationSearchV3.Models;

namespace EducationSearchV3.Repositories
{
    public interface ICountryRepository
    {
        public Task<List<Country>> GetAllCountries();
        public Task<Country?> GetCountryById(int id);
        public Task<Country?> GetCountryByName(string name);
        public Task<bool> HasCountryWithName(string name);
        public Task AddCountry(Country country);
        public Task RemoveCountry(Country country);
        public Task SaveChangesAsync();
    }
}
