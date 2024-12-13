using CarlitosDroidWebApi.Data;
using CarlitosDroidWebApi.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarlitosDroidWebApi.Domain.Service;

public class UserService
{

    private readonly UserDbContext _userDBContext;

    public UserService(UserDbContext userDBContext)
    {
        _userDBContext = userDBContext;
    }

    public async Task<User> GetUserById(string userId)
    {
        var user = await _userDBContext.Users.Where(x => x.UserID == userId).SingleOrDefaultAsync();
        return user;
    }

    public async Task<User> FindByEmailAsync(string email)
    {
        var user = await _userDBContext.Users.Where(x => x.Email == email).SingleOrDefaultAsync();
        return user;
    }

    public async Task<List<User>> GetUsers()
    {
        var users = _userDBContext.Users;
        return await users.ToListAsync();
    }

    public async Task<User> CreateUser(User user)
    {
        var result = await _userDBContext.Users.AddAsync(user);
        await _userDBContext.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<ActionResult<User>> PutUser(User user)
    {
        var result = await _userDBContext.Users
            .FirstOrDefaultAsync(e => e.UserID == user.UserID);

        if (result != null)
        {
            result.Firstname = user.Firstname;
            result.Lastname = user.Lastname;
            result.MobileNo = user.MobileNo;

            await _userDBContext.SaveChangesAsync();

            return result;
        }

        return null;
    }

    public async Task<User> DeleteUser(string userId)
    {
        var result = await _userDBContext.Users
                .FirstOrDefaultAsync(e => e.UserID == userId);

        if (result != null)
        {
            _userDBContext.Users.Remove(result);
            await _userDBContext.SaveChangesAsync();
            return result;
        }
        return null;
    }

}
