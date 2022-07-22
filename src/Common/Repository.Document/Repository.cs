using AGTec.Common.Document.Entities;
using AGTec.Common.Repository.Document.Extensions;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace AGTec.Common.Repository.Document
{
    public abstract class Repository<T> : ReadOnlyRepository<T>, IRepository<T> where T : IDocumentEntity
    {

        public Repository(IDocumentContext context) : base(context)
        { }

        public virtual async Task<bool> Delete(T entity)
        {
            var dbEntity = await GetById(entity.Id);

            if (dbEntity == null)
                return false;

              var result = await _context
                .Collection<T>(_collectionName)
                .DeleteOneAsync(f => f.Id == entity.Id);

            return result.IsAcknowledged
                && result.DeletedCount > 0;
        }

        public virtual async Task Insert(T entity)
        {
            var utcNow = DateTime.UtcNow;

            entity.Created = utcNow;
            entity.LastUpdated = utcNow;

            entity.SetSchemaVersion();

            await _context
                    .Collection<T>(_collectionName)
                    .InsertOneAsync(entity);
        }

        public virtual async Task<bool> Update(T entity)
        {
            var dbEntity = await GetById(entity.Id);

            if (dbEntity == null)
                return false;

            entity.Created = dbEntity.Created;
            entity.LastUpdated = DateTime.UtcNow;

            entity.SetSchemaVersion();

            var updateResult = await _context
                .Collection<T>(_collectionName)
                .ReplaceOneAsync(
                    filter: f => f.Id == entity.Id,
                    replacement: entity);

            return updateResult.IsAcknowledged
                && updateResult.ModifiedCount > 0;
        }
    }
}
