using System.ComponentModel.DataAnnotations;

namespace UserApi.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public required string Login { get; set; }

        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public required string Password { get; set; }

        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Zа-яА-ЯёЁ]+$")]
        public required string Name { get; set; }

        [Range(0, 2)]
        public int Gender { get; set; } = 2;
        public DateTime? Birthday { get; set; }
        public bool Admin { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public required string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? RevokedOn { get; set; }
        public string? RevokedBy { get; set; }

        public static User CreateUser(string login, string password, string name, int Gender = 2, DateTime? birthday = null, bool admin = false, string? createdBy = null)
        {
            var user = new User
            {
                Login = login,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                Name = name,
                Gender = Gender,
                Birthday = birthday,
                Admin = admin,
                CreatedBy = createdBy ?? login
            };

            return user;
        }
    }
}
