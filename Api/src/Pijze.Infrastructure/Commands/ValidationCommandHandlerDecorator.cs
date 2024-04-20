using FluentValidation;
using Pijze.Application.Common.Commands;

namespace Pijze.Infrastructure.Commands;

internal class ValidationCommandHandlerDecorator<T>(ICommandHandler<T> handler, IServiceProvider serviceProvider)
    : ICommandHandler<T>
    where T : class, ICommand
{
    public async Task HandleAsync(T command)
    {
        if (serviceProvider.GetService(typeof(IValidator<T>)) is IValidator<T> validator)
        {
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                var errorsAsString = string.Join(Environment.NewLine, validationResult.Errors.Select(error => error.ErrorMessage));
                throw new CommandValidationException(errorsAsString);
            }
        }
        
        await handler.HandleAsync(command);
    }
}

internal class ValidationCommandHandlerWithResultDecorator<T,TR>(ICommandHandler<T,TR> handler, IServiceProvider serviceProvider)
    : ICommandHandler<T,TR>
    where T : class, ICommand<TR>
{
    public async Task<TR> HandleAsync(T command)
    {
        if (serviceProvider.GetService(typeof(IValidator<T>)) is IValidator<T> validator)
        {
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                var errorsAsString = string.Join(Environment.NewLine, validationResult.Errors.Select(error => error.ErrorMessage));
                throw new CommandValidationException(errorsAsString);
            }
        }
        
        var result = await handler.HandleAsync(command);
        return result;
    }
}