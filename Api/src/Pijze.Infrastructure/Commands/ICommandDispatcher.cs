using Pijze.Application.Common.Commands;
using System.Threading.Tasks;

namespace Pijze.Infrastructure.Commands;

public interface ICommandDispatcher
{
    Task SendAsync<TCommand>(TCommand command) where TCommand : class, ICommand;
}