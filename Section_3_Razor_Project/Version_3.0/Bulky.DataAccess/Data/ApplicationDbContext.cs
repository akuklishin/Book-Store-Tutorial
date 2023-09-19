using Bulky.Models;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Data
{
    // ApplicationDbContext class inherits from the Entity Framework's DbContext class.
    // This class represents a session with the database and can be used to query and save instances of your entities.
    public class ApplicationDbContext : DbContext
    {
        // Constructor for the ApplicationDbContext.
        // Takes the database context options as a parameter which can be used to configure the DbContext.
        // The 'options' typically include connection strings, which database provider to use, etc.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // Represents the Categories table in the database. 
        // Enables CRUD operations on the table using this context.
        public DbSet<Category> Categories { get; set; }

        // Overridden method to customize the model-building phase of the database.
        // This is where you can configure entity types, relationships, etc.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring the initial seed data for the Category entity/table.
            // When the database is created/upgraded, these records will be added to the Categories table by default.
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
            );
        }
    }
}
