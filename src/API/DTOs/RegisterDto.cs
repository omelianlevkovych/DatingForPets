namespace src.API.DTOs
{
    /// <summary>
    /// The base API controller.
    /// </summary>
    public class RegisterDto
    {
        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets password.
        /// </summary>
        public string Password { get; set; }
    }
}