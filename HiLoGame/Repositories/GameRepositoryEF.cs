using HiLoGame.Context;
using HiLoGame.Repositories.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data.Entity;

namespace HiLoGame.Repositories
{
    public class GameRepositoryEF : IGameRepositoryEF
    {
        private readonly GameContext _gameContext;

        public GameRepositoryEF(GameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Delete(GameEntity entity)
        {
            _gameContext.Games.Remove(entity);
            _gameContext.SaveChanges();
        }

        public IEnumerable<GameEntity> FindAll()
        {
            return _gameContext.Games.AsNoTracking();
        }

        public GameEntity FindByid(Guid id)
        {
            GameEntity? game = _gameContext.Games.AsNoTracking()
                .FirstOrDefault(p => p.Id == id);

            _gameContext.ChangeTracker.Clear();
            return game;
        }

        public void Insert(GameEntity entity)
        {
            _gameContext.Add(entity);
            _gameContext.SaveChanges();
        }

        public void Update(GameEntity entity)
        {
            _gameContext.Games.Update(entity);
            _gameContext.SaveChanges();
        }
    }
}
