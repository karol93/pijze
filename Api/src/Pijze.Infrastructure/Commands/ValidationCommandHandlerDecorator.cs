using FluentValidation;
using Pijze.Application.Common.Commands;

namespace Pijze.Infrastructure.Commands;

internal class ValidationCommandHandlerDecorator<T> : ICommandHandler<T> where T : class, ICommand
{
    private readonly ICommandHandler<T> _handler;
    private readonly IValidator<T> _validator;
    
    public ValidationCommandHandlerDecorator(ICommandHandler<T> handler, IValidator<T> validator)
    {
        _handler = handler;
        _validator = validator;
    }

    public async Task HandleAsync(T command)
    {
        if (command == null)
            throw new ArgumentNullException(nameof(command));

        var validationResult = await _validator.ValidateAsync(command);

        if (!validationResult.IsValid)
        {
            var errorsAsString = string.Join(Environment.NewLine, validationResult.Errors.Select(error => error.ErrorMessage));
            throw new CommandValidationException(errorsAsString);
        }

        await _handler.HandleAsync(command);
    }
}