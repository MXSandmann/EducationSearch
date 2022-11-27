using EducationSearchV3.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EducationSearchV3.Data.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            //builder.HasMany(c => c.Languages).WithMany(l => l.Countries)
            //    .UsingEntity<Dictionary<string, object>>("CountryLanguage",
            //    x => x.HasOne<Language>().WithMany().OnDelete(DeleteBehavior.Cascade),
            //    x => x.HasOne<Country>().WithMany().OnDelete(DeleteBehavior.Cascade));
            builder.HasMany(c => c.Languages).WithMany(l => l.Countries);
            builder.HasMany(c => c.HighSchools).WithOne(h => h.Country);
            builder.ToTable("Countries");
        }
    }
}
