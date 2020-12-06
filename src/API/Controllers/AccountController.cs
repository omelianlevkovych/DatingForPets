using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  /// <summary>
  /// The account controller.
  /// </summary>
  public class AccountController : BaseApiController
  {
    private readonly DataContext dbContext;
    private readonly ITokenService tokenService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AccountController"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    /// <param name="tokenService">The token service.</param>
    public AccountController(
        DataContext dbContext,
        ITokenService tokenService)
        {
            this.tokenService = tokenService;
            this.dbContext = dbContext;
        }

    /// <summary>
    /// Registers the pet user.
    /// </summary>
    /// <param name="registerDto">The register dto.</param>
    /// <returns>A <see cref="PetUserEntity"/> representing the new created pet user.</returns>
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
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
            PasswordSault = hmac.Key,
        };

        dbContext.PetUsers.Add(petUser);
        await dbContext.SaveChangesAsync();

        return new UserDto
        {
            Username = petUser.Name,
            Token = tokenService.CreateToken(petUser),
        };
    }

    /// <summary>
    /// Logins the pet user.
    /// </summary>
    /// <param name="loginDto">The login dto.</param>
    /// <returns>A <see cref="PetUserEntity"/> representing the logged in pet user.</returns>
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var petUser = await dbContext.PetUsers
        .SingleOrDefaultAsync(user => user.Name == loginDto.Username);

        if (petUser == null)
        {
            return Unauthorized("Invalid username");
        }

        using var hmac = new HMACSHA512(petUser.PasswordSault);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        // Verify whether if during login user has provided the correct password.
        for (int i = 0; i < computedHash.Length; ++i)
        {
            if (computedHash[i] != petUser.PasswordHash[i])
                {
                    return Unauthorized("Invalid password");
                }
        }

        return new UserDto
        {
            Username = petUser.Name,
            Token = tokenService.CreateToken(petUser),
        };
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
