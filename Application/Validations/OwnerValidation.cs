
using Domain.Models;
using FluentValidation;

namespace Application.Validations;

public class UserValidation : AbstractValidator<User>
{
    public UserValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(20)
            .MinimumLength(5)
            .WithMessage("Name is not valid");

        //RuleFor(x => x.Password)
        //  .NotEmpty()
        //  .NotNull()
        //  .Matches("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$")
        //    .WithMessage("Password is not valid")
        //  .MinimumLength(6)
        //  .WithMessage("Password is not valid");

        //RuleFor(x => x.Email)
        //    .Matches(@"/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/")
        //      .WithMessage("Email is not valid");
    }
}
