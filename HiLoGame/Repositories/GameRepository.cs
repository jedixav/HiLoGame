using AutoMapper;
using HiLoGame.Model;
using HiLoGame.Model.Exceptions;
using HiLoGame.Repositories.Entities;

namespace HiLoGame.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly IGameRepositoryEF _gameRepositoryEF;
        private readonly IMapper _mapper;

        public GameRepository(IGameRepositoryEF gameRepositoryEF, IMapper mapper)
        {
            this._gameRepositoryEF = gameRepositoryEF;
            this._mapper = mapper;
        }

        public Guid CreateGame(GameDto newGame)
        {
            GameEntity gameEntity = _mapper.Map<GameEntity>(newGame);

            _gameRepositoryEF.Insert(gameEntity);

            return gameEntity.Id;
        }

        public void Delete(GameDto game)
        {
            _gameRepositoryEF.Delete(_mapper.Map<GameEntity>(game));
        }

        public GameDto Get(Guid id)
        {
            GameEntity gameEntity = _gameRepositoryEF.FindByid(id);
            if(gameEntity == null)
            {
                throw new GameNotFoundException();
            }

            return _mapper.Map<GameDto>(gameEntity);
        }

        public void Update(GameDto game)
        {
            _gameRepositoryEF.Update(_mapper.Map<GameEntity>(game));
        }
    }
}
