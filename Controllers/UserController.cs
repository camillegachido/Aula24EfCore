using Aula24EfCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aula24EfCore.Controllers;

// Responsavel apenas por receber e enviar dados pro cliente
// Conecta ao Service
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpGet(Name = "GetAllUsers")]
    public List<UserResponseDTO> Get()
    {
        return _service.GetUsers();
    }

    [HttpGet("orders", Name = "GetUsersWithOrders")]
    public List<UserOrdersResponseDTO> GetWithOrders()
    {
        return _service.GetUsersWithOrders();
    }
}
