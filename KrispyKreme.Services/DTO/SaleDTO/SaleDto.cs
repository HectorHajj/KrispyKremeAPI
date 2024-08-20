namespace KrispyKreme.Application.DTO.SaleDTO
{
    public class SaleDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public int DoughnutId { get; set; }
        public string DoughnutName { get; set; }
        public DateTime SaleDate { get; set; }
        public int Quantity { get; set; }
    }
}
