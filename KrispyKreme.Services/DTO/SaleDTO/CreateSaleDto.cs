namespace KrispyKreme.Application.DTO.SaleDTO
{
    public class CreateSaleDto
    {
        public int CustomerId { get; set; }
        public int DoughnutId { get; set; }
        public DateTime SaleDate { get; set; }
        public int Quantity { get; set; }
    }
}
