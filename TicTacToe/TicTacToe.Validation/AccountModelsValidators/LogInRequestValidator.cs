using TicTacToe.DTO.Models;
using FluentValidation;

namespace TicTacToe.Validation.AccountModelsValidators;

public class LogInRequestValidator : AbstractValidator<LogInRequest>
{
    public LogInRequestValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("Login is required")
            .Length(4, 20).WithMessage("Login must be between 4 and 20 characters long");
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .Length(6, 20).WithMessage("Password must be between 6 and 20 characters long");
    }
}