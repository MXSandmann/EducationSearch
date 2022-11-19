using EducationSearchV3.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace EducationSearchV3.Models
{
    public class Language
    {
        [Key]
        public int Id { get; set; }
        public Languages Name { get; set; }
        //public IEnumerable<Country>? Countries { get; set; }
    }
}
