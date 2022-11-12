using EducationSearchV3.Models;
using Microsoft.EntityFrameworkCore;

namespace EducationSearchV3.Data
{
    public static class PrepDB
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<DataContext>());
            }
        }

        private static void SeedData(DataContext context)
        {
            Console.WriteLine("-> Applying Migrations");
            context.Database.Migrate();
            try
            {

                if (!context.Countries.Any())
                {
                    Console.WriteLine("-> Seeding data");
                    context.Countries.AddRange(
                        new Country
                        {
                            Name = "Germany"
                        },
                        new Country
                        {
                            Name = "Austria",
                        });
                    context.SaveChangesAsync();
                }
                else
                {
                    Console.WriteLine("-> Already have data");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
