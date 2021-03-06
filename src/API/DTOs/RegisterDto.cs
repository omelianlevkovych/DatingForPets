using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    /// <summary>
    /// The register dto.
    /// </summary>
    public class RegisterDto
    {
        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets password.
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}