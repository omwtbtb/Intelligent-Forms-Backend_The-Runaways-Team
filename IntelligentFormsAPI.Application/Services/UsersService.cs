using AutoMapper;
using IntelligentFormsAPI.Application.Interfaces;
using IntelligentFormsAPI.Application.Models;
using IntelligentFormsAPI.Application.Models.User;
using IntelligentFormsAPI.Domain.Entities;
using IntelligentFormsAPI.Infrastructure.Interfaces;
using Newtonsoft.Json;

namespace IntelligentFormsAPI.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository userRepository;
        private readonly IMapper mapper;

        public UsersService(IUsersRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<UserDto> GetUserByIdAsync(Guid id)
        {
            var user = await userRepository.GetUserByIdAsync(id);
            return mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var user = await userRepository.GetUserByEmail(email);
            return mapper.Map<UserDto>(user);

        }

        public async Task<UserSignInResponseDto> SignInAsync(UserSignInDto userLoginDto)
        {
            var user = await userRepository.GetUserByEmail(userLoginDto.EmailAddress);

            if (user is null)
                throw new UnauthorizedAccessException("Account does not exist");

            if (!user.EmailAddress.Equals(userLoginDto.EmailAddress) || !userLoginDto.Password.Equals(user.Password))
                throw new UnauthorizedAccessException("Invalid email or password");

            return mapper.Map<UserSignInResponseDto>(user);
        }

        public async Task SignUpAsync(UserSignUpDto userSignUpDto)
        {
            var user = await userRepository.GetUserByEmail(userSignUpDto.EmailAddress);
            if (user is not null)
                throw new InvalidOperationException("Account with provided email already exists");

            user = await userRepository.GetUserByNameAsync(userSignUpDto.Name);
            if (user is not null)
                throw new InvalidOperationException("Account with provided name already exists");

            user = mapper.Map<User>(userSignUpDto);

            await userRepository.CreateUserAsync(user);
        }
    }
}
