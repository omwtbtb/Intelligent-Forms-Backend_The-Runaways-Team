using IntelligentFormsAPI.Application.Models;
using IntelligentFormsAPI.Application.Models.User;
using IntelligentFormsAPI.Domain.Entities;

namespace IntelligentFormsAPI.Application.Interfaces
{
    public interface IUsersService
    {
        public Task SignUpAsync(UserSignUpDto userSignUpDto);
        public Task<UserDto> GetUserByIdAsync(Guid id);
        public Task<UserDto> GetUserByEmailAsync(string email);
        public Task<UserSignInResponseDto> SignInAsync(UserSignInDto userSignInDto);
    }
}
