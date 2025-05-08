using UserApi.Models;

namespace UserApi.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateAccessToken(User user, bool admin);
    }
}
