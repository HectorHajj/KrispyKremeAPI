namespace KrispyKreme.Application.DTO.SaleDTO
{
    public class GetSaleDto
    {
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string DoughnutName { get; set; }
        public DateTime SaleDate { get; set; }
        public int Quantity { get; set; }
    }
}
