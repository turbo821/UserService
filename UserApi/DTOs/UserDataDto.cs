namespace UserApi.DTOs
{
    public class UserDataDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public int Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public bool isActive { get; set; }
    }
}
