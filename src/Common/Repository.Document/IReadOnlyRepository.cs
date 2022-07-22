using AGTec.Common.Document.Entities;
using System;
using System.Threading.Tasks;

namespace AGTec.Common.Repository.Document
{
    public interface IReadOnlyRepository<T> : IDisposable where T : IDocumentEntity
    {
        Task<T> GetById(Guid id);
    }
}
