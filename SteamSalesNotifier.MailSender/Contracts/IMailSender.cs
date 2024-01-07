namespace SteamSalesNotifier.MailSender.Contracts
{
    public interface IMailSender
    {
        Task SendMail(string email, string body);
    }
}
