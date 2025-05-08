using UserApi.DTOs;

namespace UserApi.Interfaces
{
    public interface IUserService
    {
        Task<Guid?> Create(CreateUserDto dto, Guid creatorId);
    }
}
