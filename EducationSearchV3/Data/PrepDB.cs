using EducationSearchV3.Models;
using EducationSearchV3.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace EducationSearchV3.Data
{
    public static class PrepDB
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<DataContext>()!);
            }
        }

        private static void SeedData(DataContext context)
        {
            Console.WriteLine("-> Applying Migrations");
            context.Database.Migrate();
            try
            {
                SeedCountries(context);
                SeedSubjects(context);
                SeedLanguages(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void SeedCountries(DataContext context)
        {
            if (context.Countries.Any())
                return;

            Console.WriteLine("-> Seeding countries data");
            context.Countries.AddRange(
                new Country
                {
                    Name = "Germany"
                },
                new Country
                {
                    Name = "Austria",
                });
            context.SaveChanges();            
        }

        private static void SeedSubjects(DataContext context)
        {
            if (context.Subjects.Any())
                return;
            
            Console.WriteLine("-> Seeding subjects data");
            context.Subjects.AddRange(
                new Subject
                {
                    Name = "Civil engineering"
                },
                new Subject
                {
                    Name = "Medicine",
                },
                new Subject
                {
                    Name = "Psychology",
                });
            context.SaveChanges();            
        }

        private static void SeedLanguages(DataContext context)
        {
            if(context.Languages.Any())
                return;

            Console.WriteLine("-> Seeding languages data");

            context.AddRange(
                new Language
                {
                    Name = Languages.English,
                },
                new Language
                {
                    Name = Languages.German,
                },
                new Language
                {
                    Name = Languages.Italian
                });
            context.SaveChanges();

            
        }
    }
}
