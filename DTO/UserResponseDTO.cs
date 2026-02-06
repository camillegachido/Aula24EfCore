using Aula24EfCore.Models;

public class UserResponseDTO
{
    public Guid Id {get;set;}
    public string Email {get;set;}
    public string Nome {get;set;}
}

public class UserOrdersResponseDTO : UserResponseDTO
{
    public List<OrderResponseDTO> Pedidos {get;set; }
}