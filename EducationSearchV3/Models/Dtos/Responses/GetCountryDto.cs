namespace EducationSearchV3.Models.Dtos.Responses
{
    public class GetCountryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<string>? Languages { get; set; }
        public IEnumerable<string>? HighSchools { get; set; }
    }
}
