﻿using AGTec.Common.Document.Entities;
using AGTec.Common.Repository.Document.Configuration;
using MongoDB.Driver;
using System;

namespace AGTec.Common.Repository.Document
{
    public class DocumentContext : IDocumentContext
    {
        private readonly IMongoDatabase _db;

        public DocumentContext(IDocumentDBConfiguration configuration)
        {
            var mongoSettings = MongoClientSettings.FromConnectionString(configuration.ConnectionString);
            mongoSettings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
            _db = new MongoClient(mongoSettings).GetDatabase(configuration.Database);
        }

        public IMongoCollection<T> Collection<T>(string collectionName) where T : IDocumentEntity => _db.GetCollection<T>(collectionName, new MongoCollectionSettings { });

        #region IDisposable
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
