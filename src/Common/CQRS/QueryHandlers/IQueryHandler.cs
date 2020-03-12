using AGTec.Common.CQRS.Queries;
using System;
using System.Threading.Tasks;

namespace AGTec.Common.CQRS.QueryHandlers
{
    public interface IQueryHandler<in TQuery, TResult> : IDisposable where TQuery : IQuery<TResult>
    {
        Task<TResult> Execute(TQuery query);
    }
}
