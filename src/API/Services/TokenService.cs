using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    /// <inheritdoc />
  public class TokenService : ITokenService
  {
    private readonly SymmetricSecurityKey key;

    /// <summary>
    /// Initializes a new instance of the <see cref="TokenService"/> class.
    /// </summary>
    /// <param name="config">The configuration.</param>
    public TokenService(IConfiguration config)
    {
        key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
    }

    /// <inheritdoc />
    public string CreateToken(PetUserEntity user)
    {
      var claims = new List<Claim>
      {
          new Claim(JwtRegisteredClaimNames.NameId, user.Name),
      };

      var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

      var tokenDescriptor = new SecurityTokenDescriptor
      {
          Subject = new ClaimsIdentity(claims),
          Expires = DateTime.UtcNow.AddDays(7),
          SigningCredentials = credentials,
      };

      var tokenHandler = new JwtSecurityTokenHandler();

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
    }
  }
}