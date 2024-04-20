using Pijze.Domain.SeedWork;

namespace Pijze.Infrastructure.Commands;

public class CommandValidationException(string message) : PijzeException(message);