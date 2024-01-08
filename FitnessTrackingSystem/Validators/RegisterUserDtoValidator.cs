using FluentValidation;
using FitnessTrackingSystem.Dto;
using FitnessTrackingSystem.Data;

namespace FitnessTrackingSystem.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(DataContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty() // znaczy, że jest wymagany
                .EmailAddress(); // podana przez użytkownika wartość musi być w formacie adresu email

            RuleFor(x => x.Password).MinimumLength(8);

            RuleFor(x => x.ConfirmPassword).Equal(e => e.Password);

            // customowy kod, dzięki któremu nie pozwalamy rejestrować się drugi raz na ten sam email
            RuleFor(c => c.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(e => e.Email == value);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "That email is taken");
                    }
                });
        }
    }
}
