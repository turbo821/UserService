using System.ComponentModel.DataAnnotations;

namespace UserApi.DTOs
{
    public class UserLoginDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public required string Login { get; set; }
    }
}
