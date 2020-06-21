using AutoMapper;
using Snake.Contract.Models;
using Snake.Game.Shapes;

namespace Snake.Game.Automapper
{
    public class SnakeViewModelProfile : Profile
    {
        public SnakeViewModelProfile()
        {
            CreateMap<SnakePart, SnakePartShape>()
                .ForMember(dest =>
                    dest.XLogicalPosition,
                    opt => opt.MapFrom(src => src.X))
                .ForMember(dest =>
                    dest.YLogicalPosition,
                    opt => opt.MapFrom(src => src.Y))
                .ReverseMap();
        }
    }
}
