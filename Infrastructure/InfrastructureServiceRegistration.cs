using Domain.Interfaces.Repository;
using Infrastructure.Configuration.ElasticsearchConfiguration;
using Infrastructure.Persistance.ElasticsearchContext;
using Infrastructure.Persistance.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IPropertyRepository, PropertyRepository>();

            //elasticsearch
            services.AddTransient<IElasticContextProvider, ElasticContextProvider>();
            services.AddSingleton<IElasticConfigurationService, ElasticConfigurationService>();

            CommonConfigurationsHelper.CommonConfig(configuration);

            return services;
        }
    }
}