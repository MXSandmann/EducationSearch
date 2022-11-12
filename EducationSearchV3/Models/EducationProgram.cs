using EducationSearchV3.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace EducationSearchV3.Models
{
    public class EducationProgram
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public StudyLevels StudyLevel { get; set; }
        public string Requirements { get; set; } = string.Empty;
        public EducationsForms EducationForm { get; set; }
        public IEnumerable<Subject> Subjects { get; set; } = null!;
        public IEnumerable<Language> Languages { get; set; } = null!;
        public HighSchool HighSchool { get; set; } = null!;
    }
}
