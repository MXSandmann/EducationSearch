using EducationSearchV3.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EducationSearchV3.Data.Configurations
{
    public class EducationProgramConfiguration : IEntityTypeConfiguration<EducationProgram>
    {
        public void Configure(EntityTypeBuilder<EducationProgram> builder)
        {
            builder.HasMany(ep => ep.Languages);
            builder.HasMany(ep => ep.Subjects).WithMany(s => s.Programs);
            builder.HasOne(ep => ep.HighSchool);
            builder.ToTable("EducationPrograms");
        }
    }
}
