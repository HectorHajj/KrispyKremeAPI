namespace KrispyKreme.Data.Entities
{
    public class Doughnut
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public virtual ICollection<Sale>? Sales { get; set; }
    }
}
