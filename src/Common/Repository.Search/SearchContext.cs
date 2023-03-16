using System;
using System.Linq;
using AGTec.Common.Repository.Search.Configuration;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;

namespace AGTec.Common.Repository.Search;

public class SearchContext: ISearchContext
{
    private readonly ElasticsearchClient _client;
    
    public SearchContext(ISearchDbConfiguration configuration)
    {
        var pool = new StaticNodePool(configuration.Hosts.Select(host => new Uri(host)));
        var settings = new ElasticsearchClientSettings(pool)
            .EnableDebugMode()
            .CertificateFingerprint(configuration.CertificateFingerprint)
            .Authentication(new BasicAuthentication(configuration.Username, configuration.Password));

        _client = new ElasticsearchClient(settings);
    }

    public ElasticsearchClient Client => _client;

    #region IDisposable
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
    #endregion
}
