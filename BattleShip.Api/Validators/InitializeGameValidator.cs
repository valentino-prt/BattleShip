using BattleShip.Models;
using BattleShip.Models.Requests;
using FluentValidation;

namespace BattleShip.Api.Validators;

public class InitializeGameValidator : AbstractValidator<InitializeGameRequest>
{
    public InitializeGameValidator()
    {
        RuleFor(request => request.CreatorId).NotEmpty().WithMessage("Creator ID cannot be empty");

        RuleFor(request => request.GameSettings).NotNull().WithMessage("Game settings cannot be null");

        RuleFor(request => request.GameSettings.Mode).IsInEnum().WithMessage("Game mode is invalid");

        When(request => request.GameSettings.Mode == GameMode.SoloVsAi, () =>
        {
            RuleFor(request => request.GameSettings.Difficulty)
                .NotNull()
                .WithMessage("Difficulty must be set in SoloVsAi mode");

            RuleFor(request => request.GameSettings.Difficulty)
                .IsInEnum()
                .When(request => request.GameSettings.Difficulty.HasValue)
                .WithMessage("Difficulty is invalid");
        });
    }
}