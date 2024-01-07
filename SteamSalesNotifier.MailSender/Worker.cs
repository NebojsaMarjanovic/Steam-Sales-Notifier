using SteamSalesNotifier.Shared.Models.Output;
using SteamSalesNotifier.Shared.RabbitMq.Contracts;

namespace SteamSalesNotifier.MailSender
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IReceiverChannel<MailNotification> _receiverChannel;

        public Worker(ILogger<Worker> logger, IReceiverChannel<MailNotification> receiverChannel)
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
