using FluentValidation;
using TicTacToe.DTO.Models;

namespace TicTacToe.Validation.SettingsModelsValidators;

public class ChangeEmailRequestValidator : AbstractValidator<ChangeEmailRequest>
{
    public ChangeEmailRequestValidator()
    {
        RuleFor(x => x.EmailInput)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is not valid")
            .MinimumLength(4).WithMessage("Email must be at least 4 characters long");
    }
}