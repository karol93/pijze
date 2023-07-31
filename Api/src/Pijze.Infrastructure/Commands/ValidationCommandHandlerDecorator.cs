using FluentValidation;
using Pijze.Application.Common.Commands;

namespace Pijze.Infrastructure.Commands;

internal class ValidationCommandHandlerDecorator<T> : ICommandHandler<T> where T : class, ICommand
{
    private readonly ICommandHandler<T> _handler;
    private readonly IServiceProvider _serviceProvider;
    
    public ValidationCommandHandlerDecorator(ICommandHandler<T> handler, IServiceProvider serviceProvider)
    {
        _handler = handler;
        _serviceProvider = serviceProvider;
    }

    public async Task HandleAsync(T command)
    {
        if (command == null)
            throw new ArgumentNullException(nameof(command));

        if (_serviceProvider.GetService(typeof(IValidator<T>)) is IValidator<T> validator)
        {
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                var errorsAsString = string.Join(Environment.NewLine, validationResult.Errors.Select(error => error.ErrorMessage));
                throw new CommandValidationException(errorsAsString);
            }
        }
        
        await _handler.HandleAsync(command);
    }
}