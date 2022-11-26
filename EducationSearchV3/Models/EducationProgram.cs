using EducationSearchV3.Models.Enums;

namespace EducationSearchV3.Models
{
    public class EducationProgram : Entity
    {
        public string Name { get; set; } = string.Empty;
        public StudyLevels StudyLevel { get; set; }
        public string Requirements { get; set; } = string.Empty;
        public EducationsForms EducationForm { get; set; }
        public ICollection<Subject> Subjects { get; set; } = null!;
        public ICollection<Language> Languages { get; set; } = null!;
        public HighSchool HighSchool { get; set; } = null!;
    }
}
