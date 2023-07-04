using FluentValidation;
using Pijze.Application.Common.Commands;

namespace Pijze.Application.Beers.Commands;

public record AddBeer(string Name, Guid BreweryId, int Rating, string Photo) : ICommand
{
    public class AddBeerValidator : AbstractValidator<AddBeer>
    {
        public AddBeerValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Rating).GreaterThanOrEqualTo(1).LessThanOrEqualTo(5);
            RuleFor(x => x.Photo).NotEmpty();
        }
    }
}
