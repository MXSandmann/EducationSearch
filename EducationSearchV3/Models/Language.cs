using EducationSearchV3.Models.Enums;

namespace EducationSearchV3.Models
{
    public class Language : Entity
    {
        public Languages Name { get; set; }
        public ICollection<Country> Countries { get; set; } = null!;
        public ICollection<EducationProgram> EducationPrograms { get; set; } = null!;
    }
}
