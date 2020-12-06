using API.Entities;

namespace API.Interfaces
{
    /// <summary>
    /// The interface for defining token service.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Creates the application user token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The json web token.</returns>
        string CreateToken(PetUserEntity user);
    }
}