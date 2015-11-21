namespace TicTacToe.Web
{
    using TicTacToe.Models;
    using DataModels;

    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.CreateMap<Game, GameInfoDataModel>()
                        .ForMember(dest => dest.FirstPlayerName,
                        opts => opts.MapFrom(src => src.FirstPlayer.UserName))
                        .ForMember(dest => dest.SecondPlayerName,
                        opts => opts.MapFrom(src => src.SecondPlayer.UserName));
        }
    }
}