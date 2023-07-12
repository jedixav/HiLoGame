namespace HiLoGame.Model.Exceptions
{
    public interface ITranslatableException
    {
        String ResourceKey { get; }
        object[]? MsgParams { get; set; }
    }
}
