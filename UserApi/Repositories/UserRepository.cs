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

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await _context.Users
                .Where(u => u.RevokedOn == null)
                .OrderBy(u => u.CreatedOn)
                .ToListAsync();

            return users;
        }

        public async Task<IEnumerable<User>> GetAllByOverAge(int age)
        {
            var currentDate = DateTime.UtcNow;
            var targetDate = currentDate.AddYears(-age);

            var users = await _context.Users
                .Where(u => u.RevokedOn == null &&
                           u.Birthday != null &&
                           u.Birthday.Value.Year <= targetDate.Year &&
                           (u.Birthday.Value.Year < targetDate.Year ||
                            (u.Birthday.Value.Month < targetDate.Month ||
                             (u.Birthday.Value.Month == targetDate.Month &&
                              u.Birthday.Value.Day <= targetDate.Day))))
                .OrderBy(u => u.CreatedOn)
                .ToListAsync();

            return users;
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

        public async Task<bool> Update(User user)
        {
            _context.Users.Update(user);
            return await Save();
        }

        public async Task<bool> Delete(User user)
        {
            _context.Users.Remove(user);
            return await Save();
        }

        public async Task<bool> isActive(Guid userId)
        {
            return await _context.Users.AnyAsync(u => u.Id == userId && u.RevokedOn == null);
        }

        private async Task<bool> Save()
        {
            var result = await _context.SaveChangesAsync();

            return result > 0 ? true : false;
        }
    }
}
