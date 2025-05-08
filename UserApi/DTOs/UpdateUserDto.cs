using System.ComponentModel.DataAnnotations;

namespace UserApi.DTOs
{
    public class UpdateUserDto
    {
        public Guid Id { get; set; }

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

        [Range(0, 2)]
        public int Gender { get; set; } = 2;
        public DateTime? Birthday { get; set; }
        public bool Admin { get; set; }
    }
}
