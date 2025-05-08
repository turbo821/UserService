using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UserApi.Data;
using UserApi.Models;

namespace UserApi.Services
{
    public class AdminInitializer
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<AdminInitializer> _logger;
        private readonly AdminCredentials _options;

        public AdminInitializer(ApplicationContext context, ILogger<AdminInitializer> logger, IOptions<AdminCredentials> options)
        {
            _context = context;
            _logger = logger;
            _options = options.Value;
        }
        public async Task InitializeAsync()
        {

            if (await _context.Users.AnyAsync(u => u.Admin))
            {
                _logger.LogInformation("Admin user already exists");
                return;
            }

            var admin = User.CreateUser(_options.Login, _options.Password, _options.Name, admin: true);

            await _context.Users.AddAsync(admin);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Admin user created successfully");
        }
    }
}
