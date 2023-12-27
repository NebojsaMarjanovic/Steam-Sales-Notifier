using Microsoft.AspNetCore.Mvc.RazorPages;
using SteamSalesNotifier.Shared.Models;

namespace SteamSalesNotifier.Formatter.Templates
{
    public class MailTemplateModel : PageModel
    {
        public List<Game> Games { get; set; }

        public void OnGet()
        {
        }
    }
}
