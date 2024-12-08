using CarlitosDroidWebApi.Domain.Models;
using CarlitosDroidWebApi.Domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace CarlitosDroidWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService userService;

    public UserController(UserService userService)
    {
        this.userService = userService;
    }

    [HttpGet("{id}/{mensaje}")]
    public async Task<ActionResult<User>> GetUserById(int id, String mensaje)
    {
        var result = await userService.GetUserById(id);
        if (result != null)
        {
            return result;
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet]
    [Route("Usuarios")]
    public async Task<ActionResult<IEnumerable<User>>> GetUser()
    {
        IEnumerable<User> users;
        users = await userService.GetUsers();
        return Ok(users);
    }

    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user)
    {
        var result = await userService.AddUser(user);
        if (result != null)
        {
            return result;
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<User>> UpdateUser(int id, User user)
    {

        var userToUpdate = await userService.GetUserById(id);

        if (userToUpdate == null)
        {
            return NotFound($"User with ID = {id} not found");
        }

        return await userService.PutUser(user);

    }

    /**
     * Remember this will response with 204 because we cannot response the deleted object
     */
    [HttpDelete("{id}")]
    public async Task<ActionResult<User>> DeleteUser(int id)
    {

        try
        {
            var userToDelete = await userService.DeleteUser(id);

            if (userToDelete == null)
            {
                return NotFound($"User con ID = {id} not found");
            }
            return await userService.DeleteUser(id);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
        }
    }
}

