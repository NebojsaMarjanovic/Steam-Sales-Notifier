using AutoMapper;

namespace SteamSalesNotifier.Crawler.Mappers
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<SteamSalesNotifier.Crawler.Models.Game, SteamSalesNotifier.Shared.Models.Game>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Discounted, opt => opt.MapFrom(src => src.IsDiscounted))
                .ForMember(dest => dest.DiscountPercent, opt => opt.MapFrom(src => src.DiscountPercent))
                .ForMember(dest => dest.OriginalPrice, opt => opt.MapFrom(src => (double)(src.OriginalPrice / (double)100)))
                .ForMember(dest => dest.FinalPrice, opt => opt.MapFrom(src => (double)(src.FinalPrice / (double)100)))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image));
        }
    }
}
