using ContactListWebApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactListWebApp.Data
{
    public static class DataSeeder
    {
        public static void SeedSampleData(ContactDbContext context)
        {
            if (context.Contacts.Any()) return;

            context.Contacts.AddRange(
                new Contact
                {
                    Id = new Guid("7ae5234a-050b-45ff-8378-4704c9a9801c"),
                    Name = "Jan",
                    Surname = "Kowalski",
                    Email = "jan.kowalski@example.com",
                    Password = "SecurePass123!+",
                    PhoneNumber = "+48123456789",
                    DateOfBirth = new DateOnly(1990, 5, 15),
                    CategoryId = 1,
                    SubcategoryId = 1
                },
                new Contact
                {
                    Id = new Guid("4f4e9f06-f15c-49ff-b7cb-31526bf19c1e"),
                    Name = "Maria",
                    Surname = "Nowak",
                    Email = "maria.nowak@example.com",
                    Password = "SecurePass456!*",
                    PhoneNumber = "+48987654321",
                    DateOfBirth = new DateOnly(1992, 3, 20),
                    CategoryId = 2,
                    SubcategoryId = null
                },
                new Contact
                {
                    Id = new Guid("25cd2c6c-e3c7-44da-9396-66b9b742406f"),
                    Name = "Piotr",
                    Surname = "Lewandowski",
                    Email = "piotr.lewandowski@example.com",
                    Password = "SecurePass789!$",
                    PhoneNumber = "+48555666777",
                    DateOfBirth = new DateOnly(1988, 7, 10),
                    CategoryId = 3,
                    CustomSubcategory = "Driver"
                }
            );

            try 
            {
                context.SaveChanges();
            }
            catch (DbUpdateException) 
            {
                Console.WriteLine("An error occurred while updating the database.");
            }
        }
    }
}
