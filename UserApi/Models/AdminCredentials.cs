using System.ComponentModel.DataAnnotations;

namespace UserApi.Models
{
    public class AdminCredentials
    {
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public required string Login { get; set; }

        [Required]
        [StringLength(100)]
        public required string Password { get; set; }

        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Zа-яА-ЯёЁ]+$")]
        public required string Name { get; set; }
    }
}
