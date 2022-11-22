namespace EducationSearchV3.Models.Dtos
{
    public class CountryDto
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<int> LanguageIds { get; set; } = null!;
        public List<int>? HighSchoolIds { get; set; } = null!;
    }
}
