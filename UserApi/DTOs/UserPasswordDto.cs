using System.ComponentModel.DataAnnotations;

namespace UserApi.DTOs
{
    public class UserPasswordDto
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public required string Password { get; set; }
    }
}
