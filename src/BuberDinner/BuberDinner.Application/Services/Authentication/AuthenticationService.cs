using BuberDinner.Application.Common.Interfaces.Authentication;

namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService(IJwtTokenGenerator jwtTokenGenerator) : IAuthenticationService
{
    public AuthenticationResult Login(string email, string password)
    {
        Guid userId = Guid.NewGuid();
        string firstName = "John";
        string lastName = "Doe";
        string token = jwtTokenGenerator.GenerateToken(userId, "firstName", "lastName");
        
        return new AuthenticationResult(
            userId,
            firstName,
            lastName,
            email,
            token);
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        Guid userId = Guid.NewGuid();
        
        var token = jwtTokenGenerator.GenerateToken(userId, firstName, lastName);
        
        return new AuthenticationResult(
            userId,
            firstName,
            lastName,
            email,
            token);
    }
}
