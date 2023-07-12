using HiLoGame.Repositories;
using HiLoGame.Model;
using HiLoGame.Model.Exceptions;

namespace HiLoGame.Services
{
    public class HiLoGameService : IHiLoGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IConfiguration _configuration;

        public HiLoGameService(IGameRepository gameRepository, IConfiguration configuration)
        {
            this._gameRepository = gameRepository;
            this._configuration = configuration;
        }

        public async Task<Guid> CreateGame()
        {
            int minBound = Int32.Parse(_configuration["Properties:Min"]);
            int maxBound = Int32.Parse(_configuration["Properties:Max"]);
            int numberToGuess = new Random().Next(minBound, maxBound +1);

            GameDto gameDto = new GameDto(numberToGuess);

            return _gameRepository.CreateGame(gameDto);
        }

        public async Task<GameGuessResult> Guess(Guid gameId, int number)
        {
            GameGuessResultTypeEnum gameGuessResultEnum;
            GameDto gameDto = _gameRepository.Get(gameId);

            CheckNumberIsValid(number);
            VerifyGameStatus(gameDto);

            gameDto.NumberOfTry++;
            switch(number.CompareTo(gameDto.NumberToGuess))
            {
                case <0:
                    gameGuessResultEnum = GameGuessResultTypeEnum.HI;
                    break;
                case 0:
                    gameGuessResultEnum = GameGuessResultTypeEnum.OK;
                    gameDto.Status = GameStatusEnum.FINISHED;
                    break;
                case >0:
                    gameGuessResultEnum = GameGuessResultTypeEnum.LO;
                    break;
            }

            _gameRepository.Update(gameDto);

            return new GameGuessResult { GameGuessResultEnum = gameGuessResultEnum, NumberOfTry = gameDto.NumberOfTry};
        }

        private void CheckNumberIsValid(int  number)
        {
            int minBound = Int32.Parse(_configuration["Properties:Min"]);
            int maxBound = Int32.Parse(_configuration["Properties:Max"]);
            if (number < minBound || number > maxBound)
            {
                object[] msgParams = { minBound, maxBound };
                throw new GuessNotInRangeException(msgParams);
            }
        }

        private void VerifyGameStatus(GameDto gameDto)
        {
            if (gameDto.Status == GameStatusEnum.CREATED)
            {
                gameDto.Status = GameStatusEnum.ONGOING;
            } else if (gameDto.Status == GameStatusEnum.FINISHED)
            {
                throw new GameClosedException();
            }
        }
    }
}
