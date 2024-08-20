namespace KrispyKreme.Data.Entities
{
    public class Sale
    {
        public int Id { get; set; }
        public DateTime SaleDate { get; set; }
        public int CustomerId { get; set; }
        public int DoughnutId { get; set; }
        public int Quantity { get; set; }

        public virtual required Customer Customer { get; set; }
        public virtual required Doughnut Doughnut { get; set; }
    }
}
