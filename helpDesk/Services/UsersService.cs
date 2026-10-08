using helpDesk.Dtos;
using helpDesk.Exceptions;
using helpDesk.Models;
using helpDesk.Repositories;

namespace helpDesk.Services
{
    public class UsersServices
    {
        private readonly IUserRepository _repository;

        public UsersServices(IUserRepository repository)
        {
            _repository = repository;
        }

        public List<UserDto> GetAll()
        {
            var users = _repository.GetAll();

            var result = users.Select(user => new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            }).ToList();

            return result;
        }

        public UserDto GetById(int id)
        {
            var user = _repository.GetById(id);

            if (user == null)
            {
                throw new NotFoundException("Id do usuário não encontrado.");
            }

            var result = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };

            return result;
        }

        public UserDto Create(CreateUserDto dto)
        {
            var PasswordHash = dto.Password; //aplicar o hash dps

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = PasswordHash,
                Role = dto.Role
            };

            var createdUser = _repository.Create(user);

            var result = new UserDto
            {
                Id = createdUser.Id,
                Name = createdUser.Name,
                Email = createdUser.Email,
                Role = createdUser.Role,
                CreatedAt = createdUser.CreatedAt
            };

            return result;
        }

        public UserDto Update(int id, UpdateUserDto dto)
        {
            var PasswordHash = dto.Password; //aplicar o hash dps

            var userUpdate = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = PasswordHash,
                Role = dto.Role
            };

            var user = _repository.Update(id, userUpdate);

            if (user == null)
            {
                throw new NotFoundException("Id do usuário não encontrado.");
            }

            var result = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };

            return result;
        }

        public UserDto Delete(int id)
        {
            var user = _repository.Delete(id);

            if (user == null)
            {
                throw new NotFoundException("Id do usuário não encontrado.");
            }

            var result = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };

            return result;
        }
    }
}