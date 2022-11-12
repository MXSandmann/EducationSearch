using System.ComponentModel.DataAnnotations;

namespace EducationSearchV3.Models
{
    public class Language
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<Country>? Countries { get; set; }
    }
}
