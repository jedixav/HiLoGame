namespace HiLoGame.Model.Exceptions
{
    public class GuessNotInRangeException : TranslatableException
    {
        public GuessNotInRangeException(object[] msgParams) : base("exception.notInRange", msgParams) { }
    }
}
