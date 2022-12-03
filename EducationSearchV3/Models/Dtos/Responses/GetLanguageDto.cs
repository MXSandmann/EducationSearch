using EducationSearchV3.Models.Enums;

namespace EducationSearchV3.Models.Dtos.Responses
{
    public class GetLanguageDto
    {
        public string Name { get; set; } = string.Empty;
        public IEnumerable<string> Countries { get; set; } = null!;
        public IEnumerable<string> EducationPrograms { get; set; } = null!;
    }
}
