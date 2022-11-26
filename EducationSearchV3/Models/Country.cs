namespace EducationSearchV3.Models
{
    public class Country : Entity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<Language> Languages { get; set; } = null!;
        public ICollection<HighSchool> HighSchools { get; set; } = null!;
    }
}
