using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using FitnessTrackingSystem.Data;
using FitnessTrackingSystem.Dto;
using FitnessTrackingSystem.Exceptions;
using FitnessTrackingSystem.Interfaces;
using FitnessTrackingSystem.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FitnessTrackingSystem.Repository
{
    public class AccountService : IAccountService
    {
        private readonly DataContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountService(DataContext context,
            IPasswordHasher<User> passwordHasher,
            AuthenticationSettings authenticationSettings)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }

        public string GenerateJwt(LoginDto loginDto)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Email == loginDto.Email);

            if (user is null)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim("Age", user.Age.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); // generowanie kredencjałów. Przekazałem klucz, który zostanie zhashowany
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);

        }

        public void RegisterUser(RegisterUserDto registerUserDto)
        {
            var newUser = new User()
            {
                Email = registerUserDto.Email,
                Age = registerUserDto.Age
            };

            var hashedPassword = _passwordHasher.HashPassword(newUser, registerUserDto.Password);

            newUser.PasswordHash = hashedPassword;
            _context.Users.Add(newUser);
            // mógłbym zapisać również wykorzystując metodę Save() zwracającą bool
            // zdefiniować w interface jak w przypadku innych repo
            _context.SaveChanges();
           
        }
    }
}
