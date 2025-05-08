using System.ComponentModel.DataAnnotations;

namespace UserApi.DTOs
{
    public class UpdateUserDataDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Zа-яА-ЯёЁ]+$")]
        public required string Name { get; set; }

        [Range(0, 2)]
        public int Gender { get; set; } = 2;
        public DateTime? Birthday { get; set; }
    }
}
