using CarlitosDroidWebApi.Domain.Models;
using CarlitosDroidWebApi.Domain.Models.Request;
using CarlitosDroidWebApi.Domain.Models.Response;
using CarlitosDroidWebApi.Domain.Service;
using CarlitosDroidWebApi.Utils;
using Microsoft.AspNetCore.Mvc;

namespace CarlitosDroidWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{

    private readonly TokenService tokenService;
    private readonly UserService userService;
    private readonly PasswordHasher passwordHandler;

    public AuthController(TokenService tokenService, UserService userService, PasswordHasher passwordHandler)
    {
        this.tokenService = tokenService;
        this.userService = userService;
        this.passwordHandler = passwordHandler;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequest registrationRequest)
    {
        var saltBytes = Security.GenerateSalt();
        // Hash the password with the salt
        string hashedPassword = passwordHandler.HashPassword(registrationRequest.Password, saltBytes);
        string base64Salt = Convert.ToBase64String(saltBytes);
        byte[] retrievedSaltBytes = Convert.FromBase64String(base64Salt);

        var user = new User
        {
            UserID = registrationRequest.UserID,
            Firstname = registrationRequest.Firstname,
            Lastname = registrationRequest.Lastname,
            Email = registrationRequest.Email,
            MobileNo = "",
            Password = base64Salt,
            ConfirmPassword = hashedPassword,
            Salt = retrievedSaltBytes,
            IsActive = true,
            LastActiondatetime = DateTime.Now,
            Role = Enums.Role.USER
        };

        await userService.CreateUser(user);
        var token = tokenService.GenerateToken(user);

        var authResponse = new AuthResponse
        {
            UserId = user.UserID,
            Username = user.Firstname,
            Token = token,
            ProfileImage = user.Lastname
        };

        return Ok(authResponse);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        User? user = await userService.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return Unauthorized("Invalid credentials 1");
        }

        var result = passwordHandler.VerifyHashedPassword(user, request.Password);
        if (!result)
        {
            return Unauthorized("Invalid credentials 2");
        }

        // Generate token
        var token = tokenService.GenerateToken(user);


        // Return auth response with token
        var authResponse = new AuthResponse
        {
            UserId = user.UserID,
            Username = user.Firstname,
            Token = token,
            ProfileImage = user.Lastname
        };
        return Ok(authResponse);
    }
}