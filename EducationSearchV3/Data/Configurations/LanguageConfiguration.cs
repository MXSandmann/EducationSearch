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
            builder.HasMany(l => l.Countries).WithMany(c => c.Languages);
                //.UsingEntity<Dictionary<string, object>>(
                //"CountryLanguage",
                //x => x.HasOne<Country>().WithMany().OnDelete(DeleteBehavior.SetNull),
                //x => x.HasOne<Language>().WithMany().OnDelete(DeleteBehavior.SetNull));
            builder.Property(l => l.Name).HasConversion(l => l.ToString(), l => Enum.Parse<Languages>(l));
            builder.ToTable("Languages");
        }
    }
}
