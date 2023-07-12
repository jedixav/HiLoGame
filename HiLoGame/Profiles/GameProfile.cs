using AutoMapper;
using HiLoGame.Model;
using HiLoGame.Repositories.Entities;

namespace HiLoGame.Profiles
{
    public class GameProfile : Profile
    {
        public GameProfile() {
            CreateMap<GameDto, GameEntity>()
                .ReverseMap();
        }
    }
}
