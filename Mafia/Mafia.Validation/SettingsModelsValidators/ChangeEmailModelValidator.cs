using FluentValidation;
using Mafia.DTO.Models;

namespace Mafia.Validation.SettingsModelsValidators;

public class ChangeEmailModelValidator : AbstractValidator<ChangeEmailModel>
{
    public ChangeEmailModelValidator()
    {
        RuleFor(x => x.EmailInput)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is not valid")
            .MinimumLength(4).WithMessage("Email must be at least 4 characters long");
    }
}