using Microsoft.AspNetCore.Mvc;

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
    public ActionResult<List<UserResponseDTO>> Get()
    {
        return Ok(_service.GetUsers());
    }

    [HttpGet("orders", Name = "GetUsersWithOrders")]
    public List<UserOrdersResponseDTO> GetWithOrders()
    {
        return _service.GetUsersWithOrders();
    }

 
    [HttpPatch("{id}", Name = "UpdateUserPassword")]
    public IActionResult UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserRequest request){
       string error = _service.UpdateUserPassword(id, request.Senha);

        if(error == "error"){
            ProblemDetails problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Usuário não existe na base de dados",
                Detail = "Usuário não foi encontrado para o id informado",
            };
            return BadRequest(problemDetails);
        }
        
        return Ok();
    }

    [HttpPost(Name = "CreateUser")]
    public async Task<IActionResult> Create([FromBody] CreateUserRequestDTO request)
    {
        try
        {
            // Validação automática via ModelState (DataAnnotations)
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Chamar o serviço para criar o usuário
            var createdUser = await _service.CreateUserAsync(request);

            // Retornar 201 Created com Location header
            return CreatedAtAction(nameof(Get), new { id = createdUser.Id }, createdUser);
        }
        catch (InvalidOperationException ex)
        {
            // Email já existe - Conflict 409
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = "Conflito",
                Detail = ex.Message,
            };
            return Conflict(problemDetails);
        }
        catch (Exception ex)
        {
            // Erro genérico - Internal Server Error 500
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Erro interno do servidor",
                Detail = ex.Message
            };
            return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
        }
    }
}
