public class OrderResponseDTO
{
    public Guid Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string PaymentType { get; set; }
    public decimal Total { get; set; }
}