using System.ComponentModel.DataAnnotations;

namespace EducationSearchV3.Models.Dtos.Requests
{
    public class CreateCountryDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public IEnumerable<int> LanguageIds { get; set; } = null!;
        public IEnumerable<int>? HighSchoolIds { get; set; }
    }

    public class UpdateCountryDto
    {
        [Required]
        public int Id { get; set; }
        public string? Name { get; set; }        
        public IEnumerable<int>? LanguageIds { get; set; }
        public IEnumerable<int>? HighSchoolIds { get; set; }
    }
}
