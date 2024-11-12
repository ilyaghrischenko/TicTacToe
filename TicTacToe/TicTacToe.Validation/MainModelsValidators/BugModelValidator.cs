using FluentValidation;
using TicTacToe.DTO.Models;

namespace TicTacToe.Validation.MainModelsValidators;

public class BugModelValidator : AbstractValidator<BugModel>
{
    public BugModelValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required");
    }
}