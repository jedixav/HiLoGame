namespace HiLoGame.Model.Exceptions
{
    public class TranslatableException : Exception, ITranslatableException
    {
        public TranslatableException(string resourceKey) : base()
        {
            ResourceKey = resourceKey;
            MsgParams = new object[] {};
        }
        public TranslatableException(string resourceKey, object[]? msgParams) : base()
        {
            ResourceKey = resourceKey;
            MsgParams = msgParams;
        }

        public string ResourceKey { get; }
        public object[]? MsgParams { get; set; }
    }
}
