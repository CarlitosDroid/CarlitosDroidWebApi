using System.Security.Cryptography;
using System.Text;
using CarlitosDroidWebApi.Domain.Models;

public class PasswordHasher
{

    public PasswordHasher() { }

    public bool VerifyHashedPassword(User userFromDatabase, string userEnteredPassword)
    {

        string storedHashedPassword = userFromDatabase.Password;// "hashed_password_from_database";
        byte[] storedSaltBytes = userFromDatabase.Salt;
        string enteredPassword = userEnteredPassword; //"user_entered_password";

        // Convert the stored salt and entered password to byte arrays
        // byte[] storedSaltBytes = Convert.FromBase64String(user.Salt);
        byte[] enteredPasswordBytes = Encoding.UTF8.GetBytes(enteredPassword);

        // Concatenate entered password and stored salt
        byte[] saltedPassword = new byte[enteredPasswordBytes.Length + storedSaltBytes.Length];
        Buffer.BlockCopy(enteredPasswordBytes, 0, saltedPassword, 0, enteredPasswordBytes.Length);
        Buffer.BlockCopy(storedSaltBytes, 0, saltedPassword, enteredPasswordBytes.Length, storedSaltBytes.Length);

        // Hash the concatenated value
        string enteredPasswordHash = HashPassword(enteredPassword, storedSaltBytes);

        // Compare the entered password hash with the stored hash
        if (enteredPasswordHash == storedHashedPassword)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public string HashPassword(string password, byte[] salt)
    {
        using (var sha256 = new SHA256Managed())
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

            // Concatenate password and salt
            Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
            Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

            // Hash the concatenated password and salt
            byte[] hashedBytes = sha256.ComputeHash(saltedPassword);

            // Concatenate the salt and hashed password for storage
            byte[] hashedPasswordWithSalt = new byte[hashedBytes.Length + salt.Length];
            Buffer.BlockCopy(salt, 0, hashedPasswordWithSalt, 0, salt.Length);
            Buffer.BlockCopy(hashedBytes, 0, hashedPasswordWithSalt, salt.Length, hashedBytes.Length);

            return Convert.ToBase64String(hashedPasswordWithSalt);
        }
    }
}