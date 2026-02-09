using Aula24EfCore.Models;
using Microsoft.EntityFrameworkCore;

// Responsável pela conexão com o banco de dados para Pedidos
public class OrderRepository : IOrderRepository
{
    private readonly PedidosContext _dbContext;

    public OrderRepository(PedidosContext context)
    {
        _dbContext = context;
    }

    public async Task<TbOrder> CreateOrderAsync(TbOrder order)
    {
        order.Id = Guid.NewGuid();
        order.CreatedAt = DateTime.UtcNow;

        _dbContext.TbOrders.Add(order);
        await _dbContext.SaveChangesAsync();

        return order;
    }

    public async Task<TbUser?> GetUserByIdAsync(Guid userId)
    {
        return await _dbContext.TbUsers.FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<TbProduct?> GetProductByIdAsync(Guid productId)
    {
        return await _dbContext.TbProducts.FirstOrDefaultAsync(p => p.Id == productId);
    }

    public async Task<TbOrder?> GetOrderByIdAsync(Guid orderId)
    {
        return await _dbContext.TbOrders
            .Include(o => o.TbOrderProducts)
            .ThenInclude(op => op.Product)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }
}
