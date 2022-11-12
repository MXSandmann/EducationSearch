using EducationSearchV3.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EducationSearchV3.Data.Configurations
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasMany(s => s.Programs).WithMany(p => p.Subjects);
            builder.ToTable("Subjects");
        }
    }
}
