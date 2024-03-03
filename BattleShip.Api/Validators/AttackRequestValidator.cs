using BattleShip.Models.Requests;
using FluentValidation;

namespace BattleShip.Api.Validators;

public class AttackRequestValidator : AbstractValidator<AttackRequest>
{
    public AttackRequestValidator()
    {
        RuleFor(request => request.GameId).NotEmpty().WithMessage("Game ID cannot be empty");

        RuleFor(request => request.PlayerId).NotEmpty().WithMessage("Player ID cannot be empty");

        RuleFor(request => request.X)
            .InclusiveBetween(0, 9)
            .WithMessage("X must be between 0 and 9");

        RuleFor(request => request.Y)
            .InclusiveBetween(0, 9)
            .WithMessage("Y must be between 0 and 9");
    }
}