namespace EducationSearchV3.Models.Dtos.Requests
{
    public class CreateUpdateHighSchoolDto
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public List<int>? ProgramIds { get; set; }
    }
}
