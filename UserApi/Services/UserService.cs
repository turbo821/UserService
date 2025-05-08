using UserApi.DTOs;
using UserApi.Interfaces;
using UserApi.Models;

namespace UserApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<Guid?> Create(CreateUserDto dto, Guid creatorId)
        {
            var creator = await _repo.GetById(creatorId);

            if (creator == null) return null;

            var user = User.CreateUser(dto.Login, dto.Password, dto.Name, dto.Gender, dto.Birthday, dto.Admin, createdBy: creator.Login);

            var userId = await _repo.Add(user);

            return userId;
        }
    }
}
