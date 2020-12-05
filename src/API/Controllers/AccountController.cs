using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.API.DTOs;

namespace src.API.Controllers
{
    /// <summary>
    /// The account controller.
    /// </summary>
    public class AccountController : BaseApiController
    {
        private readonly DataContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public AccountController(DataContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        /// <summary>
        /// Register the pet user.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [HttpPost("register")]
        public async Task<ActionResult<PetUserEntity>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username))
            {
                return BadRequest("User name is already taken");
            }

            // Using the hash algorithm for secure saving the pet username (identity) and password into database.
            using var hmac = new HMACSHA512();

            var petUser = new PetUserEntity
            {
                Name = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSault = hmac.Key
            };

            dbContext.PetUsers.Add(petUser);
            await dbContext.SaveChangesAsync();

            return petUser;
        }

        /// <summary>
        /// Look up the user.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        private async Task<bool> UserExists(string username)
        {
            return await dbContext.PetUsers.AnyAsync(user => user.Name == username.ToLower());
        }
    }
}