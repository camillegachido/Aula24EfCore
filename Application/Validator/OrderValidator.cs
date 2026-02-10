using Aula24EfCore.Models;

public static class OrderValidator
{
    public static async Task<TbProduct?> CheckIfProductExists(IOrderRepository _repository,  
        OrderProductRequestDTO? productRequest)
    {
        var product = await _repository.GetProductByIdAsync(productRequest.ProductId);
        if (product == null)
            throw new InvalidOperationException($"Produto com ID '{productRequest.ProductId}' não encontrado");

        return product;
    }
    
    public static void CheckProductStock(TbProduct? product,  OrderProductRequestDTO? productRequest)
    {
        if (product.StockQuantity.HasValue && product.StockQuantity.Value < productRequest.Quantity)
            throw new InvalidOperationException($"Estoque insuficiente para o produto '{product.Name}'. Disponível: {product.StockQuantity}");
    }
}