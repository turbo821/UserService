using UserService.Services;

namespace UserService.Extensions
{
    public static class InitialExtensions
    {
        public static async Task CreateAdmin(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                try
                {
                    var adminInitializer = scope.ServiceProvider.GetRequiredService<AdminInitializer>();
                    await adminInitializer.InitializeAsync();
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while creating the admin user");
                }
            }
        }
    }
}
