namespace EducationSearchV3.Models.Dtos
{
    public class HighSchoolDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public List<int>? ProgramIds { get; set; }
    }
}
