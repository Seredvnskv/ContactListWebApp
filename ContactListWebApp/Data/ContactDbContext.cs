using Microsoft.EntityFrameworkCore;
using ContactListWebApp.Models.Entities;

namespace ContactListWebApp.Data
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options)
        {
        }
        // Przechowywanie danych o kontaktach, kategoriach i podkategoriach w bazie danych
        public DbSet<Contact> Contacts => Set<Contact>();
        public DbSet<ContactCategory> ContactCategories => Set<ContactCategory>();
        public DbSet<ContactSubcategory> ContactSubcategories => Set<ContactSubcategory>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contact>() // Ustawienie unikalności dla pola Email
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<ContactCategory>() // Dane zgodne z instrukcją zadania
                .HasData(
                    new ContactCategory { Id = 1, CategoryName = "Business" },
                    new ContactCategory { Id = 2, CategoryName = "Private" },
                    new ContactCategory { Id = 3, CategoryName = "Other" }
                );

            modelBuilder.Entity<ContactSubcategory>()
                .HasData(
                    new ContactSubcategory { Id = 1, SubcategoryName = "Boss", ContactCategoryId = 1 },
                    new ContactSubcategory { Id = 2, SubcategoryName = "Client", ContactCategoryId = 1 },
                    new ContactSubcategory { Id = 3, SubcategoryName = "Dealer", ContactCategoryId = 1 },
                    new ContactSubcategory { Id = 4, SubcategoryName = "Partner", ContactCategoryId = 1 },
                    new ContactSubcategory { Id = 5, SubcategoryName = "Manager", ContactCategoryId = 1 }
                );
        }
    }
}
