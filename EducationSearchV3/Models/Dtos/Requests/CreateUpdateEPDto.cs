using EducationSearchV3.Models.Enums;

namespace EducationSearchV3.Models.Dtos.Requests
{
    public class CreateUpdateEPDto
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int StudyLevel { get; set; }
        public string Requirements { get; set; } = string.Empty;
        public int EducationForm { get; set; }
        public IEnumerable<int>? SubjectIds { get; set; }
        public IEnumerable<int>? LanguageIds { get; set; }
        public int HighSchoolId { get; set; }
    }
}
