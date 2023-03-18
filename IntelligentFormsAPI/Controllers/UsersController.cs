using FluentValidation;
using IntelligentFormsAPI.Application.Interfaces;
using IntelligentFormsAPI.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace IntelligentFormsAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/users"), ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> logger;
        private readonly IUsersService userService;
        private readonly IValidator<UserSignUpDto> userValidator;

        public UsersController(IUsersService userService, ILogger<UsersController> logger,
            IValidator<UserSignUpDto> userValidator)
        {
            this.userService = userService;
            this.logger = logger;
            this.userValidator = userValidator;
        }

        [HttpPost, Route("signup")]
        [SwaggerOperation(Summary = "Sign up a new user")]
        public async Task<IActionResult> SignUp(UserSignUpDto userSignUpDto)
        {
            try
            {
                var result = await userValidator.ValidateAsync(userSignUpDto);

                if (!result.IsValid)
                    return BadRequest(result.ToString());

                await userService.SignUpAsync(userSignUpDto);

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [SwaggerOperation(Summary = "Sign in a user")]
        [HttpPost, Route("signin")]
        public async Task<IActionResult> SignIn(UserSignInDto signInDto)
        {
            try
            {
                var user = await userService.SignInAsync(signInDto);

                return Ok(user);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [SwaggerOperation(Summary = "Update a user by id")]
        [HttpPatch, Route("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, dynamic patchUserDto)
        {
            try
            {
                await userService.UpdateUserAsync(id, patchUserDto);

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest();
            }
        }

        [SwaggerOperation(Summary = "Get a user by id")]
        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var userDto = await userService.GetUserById(id);

                return Ok(userDto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest();
            }
        }
    }
}
