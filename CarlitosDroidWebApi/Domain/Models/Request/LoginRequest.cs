using System.ComponentModel.DataAnnotations;

namespace CarlitosDroidWebApi.Domain.Models.Request;

public class LoginRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}