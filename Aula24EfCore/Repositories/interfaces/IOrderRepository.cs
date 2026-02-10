using Aula24EfCore.Models;

public interface IOrderRepository
{
    Task<TbOrder> CreateOrderAsync(TbOrder order);
    Task<TbProduct?> GetProductByIdAsync(Guid productId);
    Task<TbOrder?> GetOrderByIdAsync(Guid orderId);
}
