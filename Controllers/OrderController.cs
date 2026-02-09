using Microsoft.AspNetCore.Mvc;

namespace Aula24EfCore.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _service;

    public OrderController(IOrderService service)
    {
        _service = service;
    }

    [HttpPost(Name = "CreateOrder")]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequestDTO request)
    {
        try
        {
            // Validação automática via ModelState (DataAnnotations)
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdOrder = await _service.CreateOrderAsync(request);

            return CreatedAtAction(nameof(Create), new { id = createdOrder.Id }, createdOrder);
        }
        catch (InvalidOperationException ex)
        {
            // Validações de negócio (usuário não existe, produto não existe, estoque insuficiente, etc)
            // Retornar 409 Conflict ou 404 Not Found conforme o caso
            if (ex.Message.Contains("não encontrado"))
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Não Encontrado",
                    Detail = ex.Message,
                    Type = "https://httpwg.org/specs/rfc9110.html#status.404"
                };
                return NotFound(problemDetails);
            }

            if (ex.Message.Contains("insuficiente") || ex.Message.Contains("validado"))
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status409Conflict,
                    Title = "Conflito",
                    Detail = ex.Message,
                    Type = "https://httpwg.org/specs/rfc9110.html#status.409"
                };
                return Conflict(problemDetails);
            }

            // Outras validações de negócio - BadRequest 400
            var badProblemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Solicitação Inválida",
                Detail = ex.Message,
                Type = "https://httpwg.org/specs/rfc9110.html#status.400"
            };
            return BadRequest(badProblemDetails);
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
