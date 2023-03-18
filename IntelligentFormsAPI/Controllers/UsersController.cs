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
        public async Task<IActionResult> SignUpAsync(UserSignUpDto userSignUpDto)
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
        public async Task<IActionResult> SignInAsync(UserSignInDto signInDto)
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

        [SwaggerOperation(Summary = "Get a user by id")]
        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(Guid id)
        {
            try
            {
                var userDto = await userService.GetUserByIdAsync(id);

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
