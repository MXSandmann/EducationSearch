using System.ComponentModel.DataAnnotations;

namespace EducationSearchV3.Models.Dtos.Requests
{
    public class CreateEPDto
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int StudyLevel { get; set; }
        [Required]
        public string Requirements { get; set; } = string.Empty;
        [Required]
        public int EducationForm { get; set; }
        public IEnumerable<int>? SubjectIds { get; set; }
        public IEnumerable<int>? LanguageIds { get; set; }
        [Required]
        public int HighSchoolId { get; set; }
    }

    public class UpdateEPDto
    {
        [Required]
        public int Id { get; set; }        
        public string? Name { get; set; }
        public int? StudyLevel { get; set; }
        public string? Requirements { get; set; } = string.Empty;
        public int? EducationForm { get; set; }
        public IEnumerable<int>? SubjectIds { get; set; }
        public IEnumerable<int>? LanguageIds { get; set; }
        public int? HighSchoolId { get; set; }
    }
}
