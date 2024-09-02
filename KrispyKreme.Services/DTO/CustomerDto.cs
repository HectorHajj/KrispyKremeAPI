namespace KrispyKreme.Application.DTO
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string Password { get; set; }
    }
}
