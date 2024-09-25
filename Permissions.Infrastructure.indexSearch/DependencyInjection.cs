
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Nodes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Permissions.Domain.IndexSearch.Permissions;
using Permissions.Infrastructure.indexSearch.Repositories.Permissions;
using Permissions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Permissions.Infrastructure.indexSearch
{

    public static class DependencyInjection
    {
        public static IServiceCollection AddElasticsearch(this IServiceCollection services, Appsettings appsetting)
        {

            var client = new ElasticsearchClient(new Uri(appsetting?.IndexSearch?.Url ?? ""));

            Console.WriteLine($"ElasticsearchClient-URL: {appsetting?.IndexSearch?.Url}");

            services.AddSingleton<ElasticsearchClient>(client);

            services.AddScoped<IPermissionIndex, PermissionIndex>();

            return services;
        }
    }

}
