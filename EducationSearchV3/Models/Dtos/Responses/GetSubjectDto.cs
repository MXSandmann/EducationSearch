namespace EducationSearchV3.Models.Dtos.Responses
{
    public class GetSubjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<string>? Programs { get; set; }
    }
}
