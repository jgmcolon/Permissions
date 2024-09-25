using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.TransformManagement;
using Permissions.Domain.IndexDto.Permissions;
using Permissions.Domain.IndexSearch.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permissions.Infrastructure.indexSearch.Repositories.Permissions
{
    public class PermissionIndex : IPermissionIndex
    {
        private readonly ElasticsearchClient _client;
        private readonly IndexName _indexName;
        public PermissionIndex(ElasticsearchClient clientConnection)
        {
            _client = clientConnection;
            _indexName = "permission-index";
            
        }

        public async Task Add(PermissionDto item)
        {
            var response = await _client.IndexAsync<PermissionDto>(item, _indexName);

            if (response.IsValidResponse)
            {
                Console.WriteLine($"Index document with ID {response.Id} succeeded.");
            }

        }
    }
}
