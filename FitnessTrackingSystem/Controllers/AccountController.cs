using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using FitnessTrackingSystem.Dto;
using FitnessTrackingSystem.Interfaces;

namespace FitnessTrackingSystem.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] RegisterUserDto registerUserDto)
        {
            _accountService.RegisterUser(registerUserDto);
            return Ok();  
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            string token = _accountService.GenerateJwt(loginDto);
            return Ok(token);

        }
    }

}
