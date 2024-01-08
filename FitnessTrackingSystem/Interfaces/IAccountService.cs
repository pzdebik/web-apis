using FitnessTrackingSystem.Dto;

namespace FitnessTrackingSystem.Interfaces
{
    public interface IAccountService
    {
        string GenerateJwt(LoginDto loginDto);
        void RegisterUser(RegisterUserDto registerUserDto);
    }
}
