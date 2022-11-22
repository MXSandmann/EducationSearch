using System.ComponentModel.DataAnnotations;

namespace EducationSearchV3.Models
{
    public abstract class Entity
    {
        [Key]
        public virtual int Id { get; set; }
    }
}
