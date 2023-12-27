using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SteamSalesNotifier.Formatter.Configurations;
using SteamSalesNotifier.Formatter.Contracts;
using SteamSalesNotifier.Shared.Models.Output;
using SteamSalesNotifier.Shared.RabbitMq.Contracts;

namespace SteamSalesNotifier.Formatter
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IReceiverChannel<SteamSale> _receiverChannel;

        public Worker(ILogger<Worker> logger, IReceiverChannel<SteamSale> receiverChannel)
        {
            _logger = logger;
            _receiverChannel = receiverChannel;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _receiverChannel.StartReceiving();
        }
    }
}
