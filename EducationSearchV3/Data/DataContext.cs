using EducationSearchV3.Models;
using Microsoft.EntityFrameworkCore;

namespace EducationSearchV3.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<EducationProgram> EducationPrograms { get; set; } = null!;
        public DbSet<Language> Languages { get; set; } = null!;
        public DbSet<Subject> Subjects { get; set; } = null!;
        public DbSet<HighSchool> HighSchools { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        }
    }

}
