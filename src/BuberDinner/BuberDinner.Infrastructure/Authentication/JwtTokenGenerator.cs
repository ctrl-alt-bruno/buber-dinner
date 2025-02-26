using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Services;
using BuberDinner.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BuberDinner.Infrastructure.Authentication;

public class JwtTokenGenerator(
    IDateTimeProvider dateTimeProvider, 
    IOptions<JwtSettings> jwtOptions) : IJwtTokenGenerator
{
    public string GenerateToken(User user)
    {
        return new JwtSecurityTokenHandler().WriteToken(
            new JwtSecurityToken(
                issuer: jwtOptions.Value.Issuer,
                expires: dateTimeProvider.UtcNow.AddMinutes(jwtOptions.Value.ExpiryMinutes),
                claims:
                [
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                    new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                ],
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.Secret)),
                    SecurityAlgorithms.HmacSha256
                )
            )
        );
    }
}