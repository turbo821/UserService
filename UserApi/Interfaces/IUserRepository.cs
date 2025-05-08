using UserApi.Models;

namespace UserApi.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<IEnumerable<User>> GetAllByOverAge(int age);
        Task<User?> GetById(Guid id);
        Task<User?> GetByLogin(string login);
        Task<Guid?> Add(User user);
        Task<bool> Update(User user);
        Task<bool> Delete(User user);
        Task<bool> isActive(Guid userId);
    }
}
