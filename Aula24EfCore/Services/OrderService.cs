using Aula24EfCore.Models;

// Responsável pela lógica de negócio de pedidos
public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly IUserRepository _userrepository;

    public OrderService(IOrderRepository repository, IUserRepository userRepository)
    {
        _repository = repository;
        _userrepository = userRepository;
    }

    public async Task<OrderResponseDTO> CreateOrderAsync(CreateOrderRequestDTO request)
    {
        // Validar se o usuário existe
        var user = _userrepository.GetUserById(request.UserId);
        if (user == null)
            throw new InvalidOperationException($"Usuário com ID '{request.UserId}' não encontrado");

        // Validar produtos e calcular total
        decimal total = 0;
        var orderProducts = new List<TbOrderProduct>();

        foreach (var productRequest in request.Products)
        {
            var product = await OrderValidator.CheckIfProductExists(_repository, productRequest);

            OrderValidator.CheckProductStock(product, productRequest);

            // Calcular subtotal
            decimal subtotal = product.Value * productRequest.Quantity;
            total += subtotal;

            // Adicionar produto ao pedido
            orderProducts.Add(new TbOrderProduct
            {
                ProductId = productRequest.ProductId,
                Quantity = productRequest.Quantity,
                Product = product
            });
        }

        // Criar novo pedido
        var newOrder = new TbOrder
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            PaymentType = request.PaymentType,
            Total = total,
            CreatedAt = DateTime.UtcNow,
            TbOrderProducts = orderProducts,
            User = user
        };

        // Salvar no banco
        var createdOrder = await _repository.CreateOrderAsync(newOrder);

        // Retornar DTO
        return OrderMapper.MapToOrderResponseDTO(createdOrder);
    }
}
