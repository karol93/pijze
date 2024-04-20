using FluentValidation;
using Pijze.Application.Common.Commands;

namespace Pijze.Application.Breweries.Commands;

public record AddBrewery(string Name) : ICommand<Guid>
{
    public class AddBreweryValidator : AbstractValidator<AddBrewery>
    {
        public AddBreweryValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}