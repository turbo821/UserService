using Microsoft.EntityFrameworkCore;
using UserApi.Data;
using UserApi.Interfaces;
using UserApi.Models;

namespace UserApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<User?> GetById(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<User?> GetByLogin(string login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
            return user;
        }

        public async Task<Guid?> Add(User user)
        {
            _context.Users.Add(user);
            if(await Save()) return user.Id;

            return null;
        }

        public async Task<bool> IsActive(Guid id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id && u.RevokedOn == null);
        }

        private async Task<bool> Save()
        {
            var result = await _context.SaveChangesAsync();

            return result > 0 ? true : false;
        }
    }
}
