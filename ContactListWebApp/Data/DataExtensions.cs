using ContactListWebApp.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace ContactListWebApp.Data
{
    public static class DataExtensions
    {
        public static void MigrateAndSeedDatabase(this WebApplication app) // Metoda do migracji bazy danych i seeding danych
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ContactDbContext>();

            dbContext.Database.Migrate();
            DataSeeder.SeedSampleData(dbContext);
        }

        public static void AddDatabase(this WebApplicationBuilder builder) // Dpdanie konfiguracji bazy danych
        {
            builder.Services.AddDbContext<ContactDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                )
            );
        }
    }
}
