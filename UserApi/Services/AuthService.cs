using Microsoft.AspNetCore.Identity;
using UserApi.DTOs;
using UserApi.Interfaces;
using UserApi.Models;

namespace UserApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repo;
        private readonly IJwtProvider _jwtProvider;

        public AuthService(IUserRepository repo, IJwtProvider jwtProvider)
        {
            _repo = repo;
            _jwtProvider = jwtProvider;
        }
        public async Task<UserLoginResponse> Login(UserLoginRequest request)
        {
            var user = await _repo.GetByLogin(request.Login);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                return new UserLoginResponse { Success = false, Message = "Invalid credentials" };

            var accessToken = _jwtProvider.GenerateAccessToken(user, user.Admin);

            var response = new UserLoginResponse()
            {
                Success = true,
                Message = "Authenticated",
                AccessToken = accessToken
            };

            return response;
        }
    }
}
