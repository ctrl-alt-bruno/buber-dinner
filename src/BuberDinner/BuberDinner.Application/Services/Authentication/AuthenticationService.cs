using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService(
    IJwtTokenGenerator jwtTokenGenerator,
    IUserRepository userRepository) : IAuthenticationService
{
    public AuthenticationResult Login(string email, string password)
    {
        User? user = userRepository.GetUserByEmail(email);

        if (user is null)
            throw new Exception("User not found");

        if (user.Password != password)
            throw new Exception("Invalid password");
        
        string token = jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResult(
            user,
            token);
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        if (userRepository.GetUserByEmail(email) is not null)
            throw new Exception("User with this email already exists");

        User user = new User()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };

        userRepository.Add(user);

        string token = jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResult(
            user,
            token);
    }
}
