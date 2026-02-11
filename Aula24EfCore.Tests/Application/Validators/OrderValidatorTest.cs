using Aula24EfCore.Models;

[TestFixture]
public class OrderValidatorTest
{
    // Valida se o metodo CheckProductStock retorna uma exception
    // quando a quantidade é insuficiente
    [Test]
    public void OrderValidator_ShouldThrowWhenStockIsUnnavailable()
    {
        // AAA
        // Arrange
        TbProduct tbProduct = new TbProduct
        {
            Id= Guid.NewGuid(),
            Name = "Hamburguer",
            Value = 10.00m,
            StockQuantity = 2
        };

        OrderProductRequestDTO request = new OrderProductRequestDTO
        {
            ProductId = Guid.NewGuid(),
            Quantity = 3
        };

        // ASSERT
        Assert.Throws<InvalidOperationException>(() => 
            OrderValidator.CheckProductStock(tbProduct, request)
        );
    }

    [Test]
    public void OrderValidator_ShouldThrowMessageWhenStockIsUnnavailable()
    {
        TbProduct tbProduct = new TbProduct
        {
            Id= Guid.NewGuid(),
            Name = "Hamburguer",
            Value = 10.00m,
            StockQuantity = 2
        };

        OrderProductRequestDTO request = new OrderProductRequestDTO
        {
            ProductId = Guid.NewGuid(),
            Quantity = 3
        };

        var ex = Assert.Throws<InvalidOperationException>(() => 
            OrderValidator.CheckProductStock(tbProduct, request)
        );

        Assert.That(ex.Message, Is.EqualTo("Estoque insuficiente para o produto 'Hamburguer'. Disponível: 2"));
    }
    // Valida se a mensagem da exception é retornada corretamente

    // Valida se o metodo CheckProductStock não retorna uma exception
    // quando a quantidade é valida
}