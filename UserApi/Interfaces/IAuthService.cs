using UserApi.DTOs;

namespace UserApi.Interfaces
{
    public interface IAuthService
    {
        Task<UserLoginResponse> Login(SignInRequest request);
    }
}
