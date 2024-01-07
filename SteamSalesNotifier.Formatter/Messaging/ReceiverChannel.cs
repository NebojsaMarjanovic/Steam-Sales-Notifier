using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using SteamSalesNotifier.Formatter.Configurations;
using SteamSalesNotifier.Formatter.Contracts;
using SteamSalesNotifier.Shared.Models;
using SteamSalesNotifier.Shared.Models.Output;
using SteamSalesNotifier.Shared.RabbitMq.Abstractions;
using SteamSalesNotifier.Shared.RabbitMq.Configurations;
using SteamSalesNotifier.Shared.RabbitMq.Contracts;
using SteamSalesNotifier.Shared.RabbitMq.Serialization;

namespace SteamSalesNotifier.Formatter.Messaging
{
    public class ReceiverChannel : ReceiverChannelBase<SteamSale>
    {
        private readonly IFormatterService _formatterService;
        private readonly MailTemplateOptions _templateOptions;
        private readonly ISenderChannel<MailNotification> _senderChannel;

        public ReceiverChannel(IConnection connection, IOptions<ReceiverChannelConfiguration> configuration,
            IMessageSerializer<SteamSale> messageSerializer, IFormatterService formatterService, IOptions<MailTemplateOptions> templateOptions, ISenderChannel<MailNotification> senderChannel)
            : base(connection, configuration.Value, messageSerializer)
        {
            _formatterService = formatterService;
            _templateOptions = templateOptions.Value;
            _senderChannel = senderChannel;
        }


        public async override Task ProcessMessage(SteamSale message)
        {
            try
            {
                var html = await _formatterService.FormatTemplate(_templateOptions.FilePath, message.Games);
                if (html is null)
                    return;
                MailNotification mailNotification = new MailNotification(
                    Html: html,
                    Users: new List<User>
                    { 
                        new User("Nebojsa", "nebojsamarjanovic6@gmail.com"),
                        new User("Milena", "milena.rabrenovic99@gmail.com")
                    });

                _senderChannel.SendMessage(mailNotification);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
         

            
        }
    }
}
