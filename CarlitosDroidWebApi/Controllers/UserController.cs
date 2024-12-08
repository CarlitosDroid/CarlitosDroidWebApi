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
    public async Task<ActionResult<User>> GetUsuarioById(int id, String mensaje)
    {
        var result = await userService.GetUsuarioById(id);
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
    public async Task<ActionResult<IEnumerable<User>>> GetUsuarios()
    {
        IEnumerable<User> users;
        users = await userService.GetUsuarios();
        return Ok(users);
    }

    [HttpPost]
    public async Task<ActionResult<User>> PostUsuarios(User user)
    {
        var result = await userService.AddUsuario(user);
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
    public async Task<ActionResult<User>> UpdateUsuario(int id, User user)
    {

        var userToUpdate = await userService.GetUsuarioById(id);

        if (userToUpdate == null)
        {
            return NotFound($"Usuario with ID = {id} not found");
        }

        return await userService.PutUsuario(user);

    }

    /**
     * Remember this will response with 204 because we cannot response the deleted object
     */
    [HttpDelete("{id}")]
    public async Task<ActionResult<User>> DeleteUsuario(int id)
    {

        try
        {
            var userToDelete = await userService.DeleteUsuario(id);

            if (userToDelete == null)
            {
                return NotFound($"Usuario con ID = {id} not found");
            }
            return await userService.DeleteUsuario(id);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
        }
    }
}

