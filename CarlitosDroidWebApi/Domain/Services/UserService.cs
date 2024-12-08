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

    public async Task<User> GetUserById(int userId)
    {
        var user = await _userDBContext.Users.Where(x => x.Id == userId).SingleOrDefaultAsync();
        return user;
    }

    public async Task<List<User>> GetUsers()
    {
        var users = _userDBContext.Users;
        return await users.ToListAsync();
    }

    public async Task<User> AddUser(User user)
    {
        var result = await _userDBContext.Users.AddAsync(user);
        await _userDBContext.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<ActionResult<User>> PutUser(User user)
    {
        var result = await _userDBContext.Users
            .FirstOrDefaultAsync(e => e.Id == user.Id);

        if (result != null)
        {
            result.Nombre = user.Nombre;
            result.Apellido = user.Apellido;
            result.Telefono = user.Telefono;

            await _userDBContext.SaveChangesAsync();

            return result;
        }

        return null;
    }

    public async Task<User> DeleteUser(int userId)
    {
        var result = await _userDBContext.Users
                .FirstOrDefaultAsync(e => e.Id == userId);

        if (result != null)
        {
            _userDBContext.Users.Remove(result);
            await _userDBContext.SaveChangesAsync();
            return result;
        }
        return null;
    }

}
