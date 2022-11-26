namespace EducationSearchV3.Models
{
    public class Subject : Entity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<EducationProgram>? Programs { get; set; }
    }
}
