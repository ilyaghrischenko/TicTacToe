using FluentValidation;
using TicTacToe.DTO.Requests;

namespace TicTacToe.Validation.SettingsModelsValidators;

public class ChangeLoginRequestValidator : AbstractValidator<ChangeLoginRequest>
{
    public ChangeLoginRequestValidator()
    {
        RuleFor(x => x.LoginInput)
            .NotEmpty().WithMessage("This field is required")
            .Length(4, 20).WithMessage("Login must be between 4 and 20 characters long")
            .Matches("^[a-zA-Z0-9]+$").WithMessage("Login must contain only Latin letters and numbers");
    }
}