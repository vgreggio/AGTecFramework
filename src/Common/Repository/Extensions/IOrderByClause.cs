using AGTec.Common.Domain.Entities;
using System.Linq;

namespace AGTec.Common.Repository.Extensions
{ 
    public interface IOrderByClause<T> where T : IEntity
    {
        IOrderedQueryable<T> ApplySort(IQueryable<T> query, bool firstSort);
    }
}
