using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using OnlineLearningPlatform.Dto;
using OnlineLearningPlatform.Interfaces;

namespace OnlineLearningPlatform.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] RegisterUserDto registerUserDto)
        {
            _accountRepository.RegisterUser(registerUserDto);
            return Ok();  
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            string token = _accountRepository.GenerateJwt(loginDto);
            return Ok(token);

        }
    }

}
