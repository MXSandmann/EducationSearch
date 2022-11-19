using EducationSearchV3.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EducationSearchV3.Models.Enums;

namespace EducationSearchV3.Data.Configurations
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.Property(l => l.Name).HasConversion(l => l.ToString(), l => Enum.Parse<Languages>(l));
            builder.ToTable("Languages");
        }
    }
}
