using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace UserAPI.Data
{
    public static class PrepDB
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScrope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScrope.ServiceProvider.GetService<UserContext>());
            }
        }
        public static void SeedData(UserContext context)
        {
            System.Console.WriteLine("Aplicando as Migrations");
            context.Database.Migrate();
        }
    }
}