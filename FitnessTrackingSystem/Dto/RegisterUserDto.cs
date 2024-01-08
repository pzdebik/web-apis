using System.ComponentModel.DataAnnotations;

namespace FitnessTrackingSystem.Dto
{
    public class RegisterUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int Age { get; set; }
    }
}
