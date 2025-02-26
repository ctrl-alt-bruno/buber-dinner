namespace BuberDinner.Api.Controllers;

using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("auth")]
public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        AuthenticationResult registrationResult = authenticationService.Register(
            request.FirstName, 
            request.LastName, 
            request.Email, 
            request.Password);

        var response = new AuthenticationResponse(
            registrationResult.User.Id,
            registrationResult.User.FirstName,
            registrationResult.User.LastName,
            registrationResult.User.Email,
            registrationResult.Token);

        return Ok(response);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var registrationResult = authenticationService.Login(
            request.Email, 
            request.Password);

        var response = new AuthenticationResponse(
            registrationResult.User.Id,
            registrationResult.User.FirstName,
            registrationResult.User.LastName,
            registrationResult.User.Email,
            registrationResult.Token);

        return Ok(response);
    }
}