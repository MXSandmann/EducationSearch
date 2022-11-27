using System.ComponentModel.DataAnnotations;

namespace EducationSearchV3.Models.Dtos.Requests
{
    public class CreateUpdateSubjectDto
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        public IEnumerable<int>? ProgramIds { get; set; }
    }
}
