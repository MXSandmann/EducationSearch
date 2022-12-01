using EducationSearchV3.Models.Enums;

namespace EducationSearchV3.Models.Dtos.Responses
{
    public class GetEPDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string StudyLevel { get; set; } = string.Empty;
        public string Requirements { get; set; } = string.Empty;
        public string EducationForm { get; set; } = string.Empty;
        public IEnumerable<string> Subjects { get; set; } = null!;
        public IEnumerable<string> Languages { get; set; } = null!;
        public string HighSchool { get; set; } = string.Empty;
    }
}
