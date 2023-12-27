using RazorEngineCore;
using SteamSalesNotifier.Formatter.Contracts;
using SteamSalesNotifier.Formatter.Templates;
using SteamSalesNotifier.Shared.Models;

namespace SteamSalesNotifier.Formatter.Implementations
{
    internal class FormatterService : IFormatterService
    {
        public async Task<string?> FormatTemplate(string templateFilePath, List<Game> model)
        {
            var template = await RazorEngineCompiledTemplate.LoadFromFileAsync(templateFilePath);

            return await template!.RunAsync(new MailTemplateModel { Games=model});
        }
    }
}
