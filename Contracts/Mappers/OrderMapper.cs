using Aula24EfCore.Models;

public static class OrderMapper
{
    public static OrderResponseDTO MapToOrderResponseDTO(TbOrder order)
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