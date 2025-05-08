using System.ComponentModel.DataAnnotations;

namespace UserApi.DTOs
{
    public class UserDeleteDto
    {
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public required string Login { get; set; }
        public bool SoftDeletion { get; set; }
    }
}
