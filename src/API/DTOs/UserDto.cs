namespace API.DTOs
{
    /// <summary>
    /// The user dto.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the user JWT.
        /// </summary>
        public string Token { get; set; }
    }
}