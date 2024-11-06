using FluentValidation;
using TicTacToe.DTO.Models;

namespace TicTacToe.Validation.SettingsModelsValidators;

public class ChangePasswordModelValidator : AbstractValidator<ChangePasswordModel>
{
    public ChangePasswordModelValidator()
    {
        RuleFor(x => x.PasswordInput)
            .NotEmpty().WithMessage("Password is required")
            .Length(6, 20).WithMessage("Password must be between 6 and 20 characters long")
            .Matches("^[a-zA-Z0-9]+$").WithMessage("Password must contain only Latin letters and numbers");
        
        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm password is required")
            .Matches(x => x.PasswordInput).WithMessage("Passwords do not match");
    }
}