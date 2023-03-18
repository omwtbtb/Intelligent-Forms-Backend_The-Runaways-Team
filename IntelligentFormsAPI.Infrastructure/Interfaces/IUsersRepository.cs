using IntelligentFormsAPI.Domain.Entities;

namespace IntelligentFormsAPI.Infrastructure.Interfaces
{
    public interface IUsersRepository
    {
        public Task<User?> GetUserByEmail(string email);
        public Task<User> CreateUserAsync(User user);
        public Task<User?> GetUserByIdAsync(Guid id);
        public Task<User?> GetUserByNameAsync(string name);
    }
}
