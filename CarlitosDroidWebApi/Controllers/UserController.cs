using CarlitosDroidWebApi.Domain.Models;
using CarlitosDroidWebApi.Domain.Service;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet("{id}/favorites/{favoriteId}")]
    [Authorize]
    public async Task<ActionResult<User>> GetUserById(string id, String favoriteId)
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
    [Authorize]
    public async Task<ActionResult<IEnumerable<User>>> GetUser()
    {
        IEnumerable<User> users;
        users = await userService.GetUsers();
        return Ok(users);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<User>> UpdateUser(string id, User user)
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
    [Authorize]
    public async Task<ActionResult<User>> DeleteUser(string id)
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

