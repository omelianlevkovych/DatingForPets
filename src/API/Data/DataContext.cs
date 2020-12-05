using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    /// <summary>
    /// The data context.
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataContext"/> class.
        /// </summary>
        /// <param name="options">Database context options.</param>
        public DataContext(DbContextOptions options)
        : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the pet users.
        /// </summary>
        public DbSet<PetUserEntity> PetUsers { get; set; }
    }
}