using System.ComponentModel.DataAnnotations;

public class UserResponseDTO
{
    public Guid Id {get;set;}
    public string Email {get;set;}
    public string Nome {get;set;}
    public DateTime? CreatedAt {get;set;}
}

public class CreateUserRequestDTO
{
    [Required(ErrorMessage = "Name é obrigatório")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 e 100 caracteres")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password é obrigatória")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password deve ter no mínimo 6 caracteres")]
    public string Password { get; set; }
}

public class UserOrdersResponseDTO : UserResponseDTO
{
    public List<OrderResponseDTO> Pedidos {get;set; }
}

public class UpdateUserRequest
{
    public string Senha {get;set;}
}