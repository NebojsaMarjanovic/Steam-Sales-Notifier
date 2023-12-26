using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RabbitMQ.Client;

namespace SteamSalesNotifier.Shared.RabbitMq.Extensions
{
    public static class QueueingStartupExtensions
    {
        public static void AddRabbitMq(this IServiceCollection services)
        {
            services.TryAddSingleton<IConnectionFactory>(provider =>
            {
                var factoryConfig = provider.GetRequiredService<IConfiguration>().GetSection("RabbitMqConfiguration");
                var factory = new ConnectionFactory
                {
                    UserName = factoryConfig.GetSection("UserName").Value,
                    Password = factoryConfig.GetSection("Password").Value,
                    HostName = factoryConfig.GetSection("HostName").Value,
                    Port = int.TryParse(factoryConfig.GetSection("Port").Value, out int port) ? port : -1,
                    VirtualHost = factoryConfig.GetSection("VirtualHost").Value,
                    DispatchConsumersAsync = bool.TryParse(factoryConfig.GetSection("DispatchConsumersAsync").Value, out bool isAsync) ? isAsync : false,
                    AutomaticRecoveryEnabled = bool.TryParse(factoryConfig.GetSection("AutomaticRecoveryEnabled").Value, out bool shouldAutomaticRecover) ? shouldAutomaticRecover : false,
                    TopologyRecoveryEnabled = bool.TryParse(factoryConfig.GetSection("TopologyRecoveryEnabled").Value, out bool shouldTopologyRecover) ? shouldTopologyRecover : false,
                    ConsumerDispatchConcurrency = int.TryParse(factoryConfig.GetSection("ConsumerDispatchConcurrency").Value, out int concurrency) ? concurrency : 1,
                };
                return factory;
            });

            services.TryAddSingleton<IConnection>(provider =>
            {
                var connectionFactory = provider.GetRequiredService<IConnectionFactory>();

                var connection = connectionFactory.CreateConnection();

                return connection;
            });
        }
    }
}
