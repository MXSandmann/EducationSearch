namespace EducationSearchV3.Models
{
    public class HighSchool : Entity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<EducationProgram> Programs { get; set; } = null!;
        public Country Country { get; set; } = null!;
    }
}
