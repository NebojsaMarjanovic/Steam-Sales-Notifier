using AutoMapper;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SteamSalesNotifier.Crawler.Configuration;
using SteamSalesNotifier.Crawler.Contracts;
using SteamSalesNotifier.Crawler.Models;
using SteamSalesNotifier.Shared.Models;
using SteamSalesNotifier.Shared.Models.Output;
using SteamSalesNotifier.Shared.RabbitMq.Contracts;

namespace SteamSalesNotifier.Crawler
{
    public class Script : IHostedService
    {
        private readonly ICrawler _crawler;
        private readonly ILogger<Script> _logger;
        private readonly SteamApiOptions _steamApiOptions;
        private readonly IMapper _mapper;
        private readonly ISenderChannel<SteamSale> _senderChannel;

        public Script(ICrawler crawler, IOptions<SteamApiOptions> steamApiOptions, ILogger<Script> logger, IMapper mapper, ISenderChannel<SteamSale> senderChannel)
        {
            _crawler = crawler;
            _steamApiOptions = steamApiOptions.Value;
            _logger = logger;
            _mapper = mapper;
            _senderChannel = senderChannel;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            List<Shared.Models.Game> games = new();

            try
            {
                var scrapedData = await _crawler.GetData(_steamApiOptions.Url);

                foreach(var scrapedGame in scrapedData)
                {
                    games.Add(_mapper.Map<Shared.Models.Game>(scrapedGame));
                }

                var steamSale = new SteamSale(Games: games);

                _senderChannel.SendMessage(steamSale);


                _logger.LogInformation("Finish");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }        
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
