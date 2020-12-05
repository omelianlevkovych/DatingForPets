using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    /// <summary>
    /// The application pet user entity.
    /// </summary>
    [Table("PetUsers")]
    public class PetUserEntity
    {
        /// <summary>
        /// Gets or sets the pet user identifier.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Gets or sets the pet user name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the pet password hash.
        /// </summary>
        public byte[] PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the pet password sault.
        /// </summary>
        public byte[] PasswordSault { get; set; }
    }
}