using FluentValidation;
using TicTacToe.DTO.Requests;

namespace TicTacToe.Validation.MainModelsValidators;

public class SendBugRequestValidator : AbstractValidator<SendBugRequest>
{
    public SendBugRequestValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required");
    }
}