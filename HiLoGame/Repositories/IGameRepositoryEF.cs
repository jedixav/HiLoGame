using HiLoGame.Repositories.Entities;

namespace HiLoGame.Repositories
{
    public interface IGameRepositoryEF : IRepositoryEF<Guid, GameEntity>
    {

    }
}
