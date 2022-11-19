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
            var languagesDefined = Enum.GetValues(typeof(Languages));

            if (context.Languages.Count() == languagesDefined.Length)
                return;

            Console.WriteLine("-> Seeding languages data");

            var languages = new List<Language>();

            foreach (Languages item in languagesDefined)
            {
                var language = new Language
                {
                    Name = item
                };
                languages.Add(language);
            }

            context.AddRange(languages);
            context.SaveChanges();

            
        }
    }
}
