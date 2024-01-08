using OnlineLearningPlatform.Dto;

namespace OnlineLearningPlatform.Interfaces
{
    public interface IAccountRepository
    {
        string GenerateJwt(LoginDto loginDto);
        void RegisterUser(RegisterUserDto registerUserDto);
    }
}
