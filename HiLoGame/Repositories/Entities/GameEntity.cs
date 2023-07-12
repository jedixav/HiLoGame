using HiLoGame.Model;

namespace HiLoGame.Repositories.Entities
{
    public class GameEntity
    {
        public Guid Id { get; set; }
        public int NumberToGuess { get; set; }
        public int NumberOfTry { get; set; }
        public GameStatusEnum Status { get; set; }
    }
}
