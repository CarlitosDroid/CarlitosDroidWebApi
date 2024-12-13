namespace CarlitosDroidWebApi.Domain.Models;

public class User
{
    public string UserID { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string MobileNo { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public byte[] Salt { get; set; }
    public DateTime LastActiondatetime { get; set; }
    public bool IsActive { get; set; }
    public Enums.Role Role { get; set; }
}