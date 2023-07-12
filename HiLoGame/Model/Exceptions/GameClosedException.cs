namespace HiLoGame.Model.Exceptions
{
    public class GameClosedException : TranslatableException
    {
        public GameClosedException() : base("exception.gameClosed") {
        }

    }
}
