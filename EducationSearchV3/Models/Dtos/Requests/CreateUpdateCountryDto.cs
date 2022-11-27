using System.ComponentModel.DataAnnotations;

namespace EducationSearchV3.Models.Dtos.Requests
{
    public class CreateUpdateCountryDto
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public IEnumerable<int> LanguageIds { get; set; } = null!;
        public IEnumerable<int>? HighSchoolIds { get; set; } = null!;
    }
}
