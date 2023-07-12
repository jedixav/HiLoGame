namespace HiLoGame.Model.Exceptions
{
    public class GameNotFoundException : TranslatableException
    {
        public GameNotFoundException() : base("exception.gameNotFound")
        {
        }
    }
}
