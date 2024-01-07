using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using SteamSalesNotifier.MailSender.Contracts;

namespace SteamSalesNotifier.MailSender.Implementations
{
    public class MailSender : IMailSender
    {
        public async Task SendMail(string email, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Nebojsa", "nebojsamarjanovic6@gmail.com"));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = "Test";
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("nebojsamarjanovic6@gmail.com", "ailr msrm wycb nkrn");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
