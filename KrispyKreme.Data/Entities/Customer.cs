namespace KrispyKreme.Data.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string Password { get; set; }

        public virtual ICollection<Sale>? Sales { get; set; }
    }
}
