using System.ComponentModel.DataAnnotations;

namespace EducationSearchV3.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<Language> Languages { get; set; } = null!;
        public IEnumerable<HighSchool> HighSchools { get; set; } = null!;
    }
}
