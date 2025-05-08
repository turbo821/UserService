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

        public async Task<UpdateUserDataDto?> UpdateData(UpdateUserDataDto dto, Guid userId)
        {
            var currentUser = await _repo.GetById(userId);
            if (currentUser == null || currentUser.RevokedOn != null) return null;

            var updateUser = await _repo.GetById(dto.Id);
            if (updateUser == null) return null;

            bool isAdmin = currentUser.Admin && currentUser.RevokedOn == null;
            bool isSelfUpdate = currentUser.Id == updateUser.Id && currentUser.RevokedOn == null;

            if(!isAdmin && !isSelfUpdate) return null;

            updateUser.Name = dto.Name;
            updateUser.Gender = dto.Gender;
            updateUser.Birthday = dto.Birthday;

            updateUser.ModifiedOn = DateTime.UtcNow;
            updateUser.ModifiedBy = currentUser.Login;

            var success = await _repo.Update(updateUser);
            if (!success) return null;
            return dto;
        }

        public async Task<UserPasswordDto?> ChangePassword(UserPasswordDto dto, Guid userId)
        {
            var currentUser = await _repo.GetById(userId);
            if (currentUser == null || currentUser.RevokedOn != null) return null;

            var updateUser = await _repo.GetById(dto.Id);
            if (updateUser == null) return null;

            bool isAdmin = currentUser.Admin && currentUser.RevokedOn == null;
            bool isSelfUpdate = currentUser.Id == updateUser.Id && currentUser.RevokedOn == null;

            if (!isAdmin && !isSelfUpdate) return null;

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, updateUser.Password))
            {
                updateUser.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }

            updateUser.ModifiedOn = DateTime.UtcNow;
            updateUser.ModifiedBy = currentUser.Login;

            var success = await _repo.Update(updateUser);
            if (!success) return null;
            return dto;
        }

        public async Task<UserLoginDto?> ChangeLogin(UserLoginDto dto, Guid userId)
        {
            var currentUser = await _repo.GetById(userId);
            if (currentUser == null || currentUser.RevokedOn != null) return null;

            var updateUser = await _repo.GetById(dto.Id);
            if (updateUser == null) return null;

            bool isAdmin = currentUser.Admin && currentUser.RevokedOn == null;
            bool isSelfUpdate = currentUser.Id == updateUser.Id && currentUser.RevokedOn == null;

            if (!isAdmin && !isSelfUpdate) return null;

            if (updateUser.Login != dto.Login)
            {
                var existingUser = await _repo.GetByLogin(dto.Login);
                if (existingUser != null)
                    return null;
            }

            updateUser.Login = dto.Login;
            updateUser.ModifiedOn = DateTime.UtcNow;
            updateUser.ModifiedBy = currentUser.Login;

            var success = await _repo.Update(updateUser);
            if (!success) return null;
            return dto;
        }

        public async Task<IEnumerable<UserDto>?> GetAll(Guid userId)
        {
            var isActive = await _repo.isActive(userId);

            if (!isActive) return null;

            var users = await _repo.GetAll();

            var userDtos = new List<UserDto>();
            userDtos = users.Select(
                u =>
                new UserDto
                {
                    Id = u.Id, Login = u.Login,
                    Name = u.Name, Gender = u.Gender,
                    Birthday = u.Birthday, Admin = u.Admin,
                    CreatedBy = u.CreatedBy, CreatedOn = u.CreatedOn,
                    ModifiedBy = u.ModifiedBy, ModifiedOn = u.ModifiedOn
                }).ToList();

            return userDtos;
        }

        public async Task<UserDataDto?> GetByLogin(LoginDto dto, Guid userId)
        {
            var isActive = await _repo.isActive(userId);

            if(!isActive) return null;

            var user = await _repo.GetByLogin(dto.Login);

            if (user == null) return null;

            var userDto = new UserDataDto
            {
                Id = user.Id,
                Name = user.Name,
                Gender = user.Gender,
                Birthday = user.Birthday,
                isActive = user.RevokedOn == null
            };

            return userDto;
        }

        public async Task<UserDataDto?> GetByData(SignInRequest dto, Guid userId)
        {
            var isActive = await _repo.isActive(userId);

            if (!isActive) return null;

            var user = await _repo.GetByLogin(dto.Login);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                return null;
            
            var userDto = new UserDataDto
            {
                Id = user.Id,
                Name = user.Name,
                Gender = user.Gender,
                Birthday = user.Birthday,
                isActive = user.RevokedOn == null
            };

            return userDto;
        }

        public async Task<IEnumerable<UserDto>?> GetAllByOverAge(Guid userId, int age)
        {
            var isActive = await _repo.isActive(userId);

            if (!isActive) return null;

            if (age < 0) return null;
            
            var users = await _repo.GetAllByOverAge(age);

            var userDtos = new List<UserDto>();
            userDtos = users.Select(
                u =>
                new UserDto
                {
                    Id = u.Id,
                    Login = u.Login,
                    Name = u.Name,
                    Gender = u.Gender,
                    Birthday = u.Birthday,
                    Admin = u.Admin,
                    CreatedBy = u.CreatedBy,
                    CreatedOn = u.CreatedOn,
                    ModifiedBy = u.ModifiedBy,
                    ModifiedOn = u.ModifiedOn
                }).ToList();

            return userDtos;
        }

        public async Task<Guid?> Delete(UserDeleteDto dto, Guid userId)
        {
            var currentUser = await _repo.GetById(userId);
            if (currentUser == null || currentUser.RevokedOn != null) return null;

            var deleteUser = await _repo.GetByLogin(dto.Login);
            if (deleteUser == null) return null;

            var deleteUserId = deleteUser.Id;

            if (dto.SoftDeletion)
            {
                deleteUser.RevokedOn = DateTime.UtcNow;
                deleteUser.RevokedBy = currentUser.Login;

                var success = await _repo.Update(deleteUser);
                if (!success) return null;
            }
            else
            {
                var success = await _repo.Delete(deleteUser);
                if (!success) return null;
            }

            return deleteUserId;
        }

        public async Task<Guid?> Restore(LoginDto dto, Guid userId)
        {
            var currentUser = await _repo.GetById(userId);
            if (currentUser == null || currentUser.RevokedOn != null) return null;

            var restoreUser = await _repo.GetByLogin(dto.Login);
            if (restoreUser == null) return null;

            restoreUser.RevokedOn = null;
            restoreUser.RevokedBy = null;

            var success = await _repo.Update(restoreUser);
            if (!success) return null;
            
            return restoreUser.Id;
        }
    }
}
