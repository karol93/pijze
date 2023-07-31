using FluentValidation;
using Pijze.Application.Common.Commands;

namespace Pijze.Application.Beers.Commands;

public record UpdateBeer(Guid Id, string Name, Guid BreweryId, int Rating, string Photo) : ICommand
{
    public class UpdateBeerValidator : AbstractValidator<UpdateBeer>
    {
        public UpdateBeerValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Rating).GreaterThanOrEqualTo(1).LessThanOrEqualTo(5);
            RuleFor(x => x.Photo).NotEmpty();
        }
    }
}