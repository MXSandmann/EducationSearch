namespace EducationSearchV3.Models.Dtos.Responses
{
    public class GetHighSchoolDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public IEnumerable<string>? Programs { get; set; }
    }
}
