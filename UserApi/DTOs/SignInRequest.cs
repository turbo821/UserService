using System.ComponentModel.DataAnnotations;

namespace UserApi.DTOs
{
    public class SignInRequest
    {
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public required string Login { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        [StringLength(100)]
        public required string Password { get; set; }
    }
}
