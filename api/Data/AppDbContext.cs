using api.Models;
using Microsoft.EntityFrameworkCore;
namespace api.Data
{
    // The DbContext is EF Core's connection to the database
    //Our Repositories use it to read and save rows. (Data to our Database)
    public class AppDbContext : DbContext
    {
        
        //The options (which database to use, and its connection string) come from
        //Program.cs, through dependency injection. we just hand them to the DbContext
        //Base class
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }


        //Each DbSet is one table. this one becomes the Movies table
        public DbSet<Movie> Movies {get;set;}
    }
}