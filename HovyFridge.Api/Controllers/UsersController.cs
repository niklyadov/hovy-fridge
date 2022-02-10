using HovyFridge.Api.Entity;
using Microsoft.AspNetCore.Mvc;

namespace HovyFridge.Api.Controllers;

[ApiController]
public class UsersController : ControllerBase
{
    private readonly ILogger<FridgesController> _logger;

    public UsersController(ILogger<FridgesController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("[controller]")]
    public ICollection<User> GetAll()
    {
        return new List<User>()
            {
                new User() { Name = "Anchovy"}
            };
    }

    [HttpGet]
    [Route("[controller]/{id}")]
    public User GetUser([FromRoute] int id)
    {
        return new User() { Name = "Anchovy" };
    }

    [HttpDelete]
    [Route("[controller]/{id}")]
    public bool DeleteUser([FromRoute] int id)
    {
        return true;
    }

    [HttpPut]
    [Route("[controller]/{id}")]
    public User UpdateUser([FromBody] User user)
    {
        return user;
    }

    [HttpPost]
    [Route("[controller]")]
    public User AddUser([FromBody] User user)
    {
        return user;
    }
}