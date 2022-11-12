using EducationSearchV3.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EducationSearchV3.Data.Configurations
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.HasMany(l => l.Countries).WithMany(c => c.Languages);
        }
    }
}
