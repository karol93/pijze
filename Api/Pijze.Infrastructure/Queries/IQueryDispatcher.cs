using Pijze.Application.Common.Queries;
using System.Threading.Tasks;

namespace Pijze.Infrastructure.Queries;

public interface IQueryDispatcher
{
    Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
}