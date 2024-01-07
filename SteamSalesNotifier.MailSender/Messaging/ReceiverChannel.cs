using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using SteamSalesNotifier.MailSender.Contracts;
using SteamSalesNotifier.Shared.Models.Output;
using SteamSalesNotifier.Shared.RabbitMq.Abstractions;
using SteamSalesNotifier.Shared.RabbitMq.Configurations;
using SteamSalesNotifier.Shared.RabbitMq.Serialization;

namespace SteamSalesNotifier.MailSender.Messaging
{
    public class ReceiverChannel : ReceiverChannelBase<MailNotification>
    {
        IMailSender _mailSender;

        public ReceiverChannel(IConnection connection, IOptions<ReceiverChannelConfiguration> configuration,
            IMessageSerializer<MailNotification> messageSerializer, IMailSender mailSender)
            : base(connection, configuration.Value, messageSerializer)
        {
            _mailSender = mailSender;
        }

        public override async Task ProcessMessage(MailNotification message)
        {
            foreach(var user in message.Users)
            await _mailSender.SendMail(user.Mail, message.Html);
        }
    }
}
