using System.ComponentModel.DataAnnotations;

namespace EducationSearchV3.Models
{
    public class HighSchool
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<EducationProgram> Programs { get; set; } = null!;
    }
}
