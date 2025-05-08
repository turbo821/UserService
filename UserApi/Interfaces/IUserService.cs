using UserApi.DTOs;

namespace UserApi.Interfaces
{
    public interface IUserService
    {
        Task<Guid?> Create(CreateUserDto dto, Guid creatorId);
        Task<UpdateUserDataDto?> UpdateData(UpdateUserDataDto dto, Guid userId);
        Task<UserPasswordDto?> ChangePassword(UserPasswordDto dto, Guid userId);
        Task<UserLoginDto?> ChangeLogin(UserLoginDto dto, Guid userId);
        Task<IEnumerable<UserDto>?> GetAll(Guid userId);
        Task<UserDataDto?> GetByLogin(LoginDto dto, Guid userId);
        Task<UserDataDto?> GetByData(SignInRequest dto, Guid userId);
        Task<IEnumerable<UserDto>?> GetAllByOverAge(Guid userId, int age);
        Task<Guid?> Delete(UserDeleteDto dto, Guid userId);
        Task<Guid?> Restore(LoginDto dto, Guid userId);
    }
}
