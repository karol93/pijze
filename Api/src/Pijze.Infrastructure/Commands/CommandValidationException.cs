using Pijze.Domain.SeedWork;

namespace Pijze.Infrastructure.Commands;

public class CommandValidationException : PijzeException
{
    public CommandValidationException(string message) : base(message)
    {
    }
}