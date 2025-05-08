namespace UserApi.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public required string Login { get; set; }
        public required string Name { get; set; }
        public int Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public bool Admin { get; set; }
        public DateTime CreatedOn { get; set; }
        public required string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
