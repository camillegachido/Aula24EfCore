using Aula24EfCore.Models;

// Responsável pela lógica de negócio de pedidos
public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;

    public OrderService(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<OrderResponseDTO> CreateOrderAsync(CreateOrderRequestDTO request)
    {
        // Validar se o usuário existe
        var user = await _repository.GetUserByIdAsync(request.UserId);
        if (user == null)
            throw new InvalidOperationException($"Usuário com ID '{request.UserId}' não encontrado");

        // Validar produtos e calcular total
        decimal total = 0;
        var orderProducts = new List<TbOrderProduct>();

        foreach (var productRequest in request.Products)
        {
            var product = await _repository.GetProductByIdAsync(productRequest.ProductId);
            if (product == null)
                throw new InvalidOperationException($"Produto com ID '{productRequest.ProductId}' não encontrado");

            // Validar estoque
            if (product.StockQuantity.HasValue && product.StockQuantity.Value < productRequest.Quantity)
                throw new InvalidOperationException($"Estoque insuficiente para o produto '{product.Name}'. Disponível: {product.StockQuantity}");

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
        return MapToOrderResponseDTO(createdOrder);
    }

    private OrderResponseDTO MapToOrderResponseDTO(TbOrder order)
    {
        var response = new OrderResponseDTO
        {
            Id = order.Id,
            CreatedAt = order.CreatedAt,
            PaymentType = order.PaymentType,
            Total = order.Total,
            UserId = order.UserId,
            Products = order.TbOrderProducts.Select(op => new OrderProductResponseDTO
            {
                ProductId = op.ProductId,
                ProductName = op.Product.Name,
                ProductValue = op.Product.Value,
                Quantity = op.Quantity,
                Subtotal = op.Product.Value * op.Quantity
            }).ToList()
        };

        return response;
    }
}
