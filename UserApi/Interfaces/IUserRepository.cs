using UserApi.Models;

namespace UserApi.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetById(Guid id);
        Task<User?> GetByLogin(string login);
        Task<Guid?> Add(User user);
    }
}
