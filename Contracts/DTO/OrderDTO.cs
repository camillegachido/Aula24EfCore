using System.ComponentModel.DataAnnotations;

public class CreateOrderRequestDTO
{
    [Required(ErrorMessage = "UserId é obrigatório")]
    public Guid UserId { get; set; }

    [Required(ErrorMessage = "PaymentType é obrigatório")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "PaymentType deve ter entre 3 e 50 caracteres")]
    public string PaymentType { get; set; }

    [Required(ErrorMessage = "Produtos são obrigatórios")]
    [MinLength(1, ErrorMessage = "O pedido deve conter pelo menos um produto")]
    public List<OrderProductRequestDTO> Products { get; set; } = new();
}

public class OrderProductRequestDTO
{
    [Required(ErrorMessage = "ProductId é obrigatório")]
    public Guid ProductId { get; set; }

    [Required(ErrorMessage = "Quantidade é obrigatória")]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero")]
    public int Quantity { get; set; }
}

public class OrderResponseDTO
{
    public Guid Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string PaymentType { get; set; }
    public decimal Total { get; set; }
    public Guid UserId { get; set; }
    public List<OrderProductResponseDTO> Products { get; set; } = new();
}

public class OrderProductResponseDTO
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal ProductValue { get; set; }
    public int Quantity { get; set; }
    public decimal Subtotal { get; set; }
}
