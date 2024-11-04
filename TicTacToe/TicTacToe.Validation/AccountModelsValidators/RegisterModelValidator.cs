using TicTacToe.DTO.Models;
using FluentValidation;

namespace TicTacToe.Validation.AccountModelsValidators;

public class RegisterModelValidator : AbstractValidator<RegisterModel>
{
    public RegisterModelValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is not valid")
            .MinimumLength(4).WithMessage("Email must be at least 4 characters long");
        
        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("Login is required")
            .Length(4, 20).WithMessage("Login must be between 4 and 20 characters long")
            .Matches("^[a-zA-Z0-9]+$").WithMessage("Login must contain only Latin letters and numbers");
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .Length(6, 20).WithMessage("Password must be between 6 and 20 characters long")
            .Must((model, password) => password != model.Login).WithMessage("Password must not match the login")
            .Matches("^[a-zA-Z0-9]+$").WithMessage("Password must contain only Latin letters and numbers");
        
        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm password is required")
            .Matches(x => x.Password).WithMessage("Passwords do not match");
    }
}