using HiLoGame.Model;

namespace HiLoGame.Repositories
{
    public interface IGameRepository
    {
        Guid CreateGame(GameDto newGame);

        GameDto Get(Guid id);

        void Update(GameDto game);

        void Delete(GameDto game);

    }
}
