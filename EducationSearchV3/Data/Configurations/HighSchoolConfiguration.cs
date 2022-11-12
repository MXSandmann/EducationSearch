using EducationSearchV3.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EducationSearchV3.Data.Configurations
{
    public class HighSchoolConfiguration : IEntityTypeConfiguration<HighSchool>
    {
        public void Configure(EntityTypeBuilder<HighSchool> builder)
        {
            builder.HasMany(hs => hs.Programs).WithOne(p => p.HighSchool);
            builder.ToTable("HighSchools");
        }
    }
}
