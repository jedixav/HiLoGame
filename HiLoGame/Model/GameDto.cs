namespace HiLoGame.Model
{
    public class GameDto
    {
        public Guid Id { get; set; }
        public int NumberToGuess { get; set; }
        public int NumberOfTry { get; set; }
        public GameStatusEnum Status { get; set; }

        public GameDto(int numberToGuess)
        {
            this.NumberToGuess = numberToGuess;
            this.NumberOfTry = 0;
            this.Status = GameStatusEnum.CREATED;
        }
    }
}
