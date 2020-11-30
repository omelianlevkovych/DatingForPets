using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    // The data context.
    public class DataContext : DbContext
    {
        // The constructor inicializing the data context.
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        // The pets database set.
        public DbSet<Pet> Pets { get; set; }
    }
}