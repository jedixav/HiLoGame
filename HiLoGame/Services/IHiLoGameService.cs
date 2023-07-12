using HiLoGame.Model;

namespace HiLoGame.Services
{
    public interface IHiLoGameService
    {
        Task<Guid> CreateGame();

        Task<GameGuessResult> Guess(Guid gameId, int number);
    }
}
