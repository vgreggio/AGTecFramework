using AGTec.Common.CQRS.Queries;
using System.Threading.Tasks;

namespace AGTec.Common.CQRS.Dispatchers
{
    public interface IQueryDispatcher
    {
        Task<TResult> Execute<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
    }
}
