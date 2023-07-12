using Microsoft.VisualStudio.TestTools.UnitTesting;
using HiLoGame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiLoGame.Controllers;
using Moq;
using HiLoGame.Repositories;
using Microsoft.Extensions.Configuration;
using HiLoGame.Model;
using HiLoGame.Model.Exceptions;

namespace HiLoGame.Services.Tests
{
    [TestClass()]
    public class HiLoGameServiceTests
    {
        private Mock<IGameRepository> _gameRepositoryMock;
        private Mock<IConfiguration> _configurationMock;
        private HiLoGameService _hiLoGameService;
        private readonly Guid _gameId = Guid.Parse("b9442a81-587b-4dac-8ee4-23412bb1c2c6");

        private GameDto InitializeGame(GameStatusEnum gameStatusEnum)
        {
            return new GameDto(5)
            {
                Id = _gameId,
                NumberOfTry = 1,
                Status = gameStatusEnum
            };
        }

        [TestInitialize]
        public void initialize()
        {
            _gameRepositoryMock = new Mock<IGameRepository>();
            _configurationMock = new Mock<IConfiguration>();
            _hiLoGameService = new HiLoGameService(_gameRepositoryMock.Object, _configurationMock.Object);

            _configurationMock.Setup(x => x["Properties:Min"]).Returns("0");
            _configurationMock.Setup(x => x["Properties:Max"]).Returns("10");
        }

        [TestMethod]
        public async Task CreateGameTest()
        {
            await _hiLoGameService.CreateGame();
            _gameRepositoryMock.Verify(x => x.CreateGame(It.IsAny<GameDto>()), Times.Once);
        }

        [DataRow(5, GameGuessResultTypeEnum.OK)]
        [DataRow(2, GameGuessResultTypeEnum.HI)]
        [DataRow(8, GameGuessResultTypeEnum.LO)]
        [DataTestMethod]
        public async Task GuessValidNumber(int guess, GameGuessResultTypeEnum expectedResult)
        {
            _gameRepositoryMock.Setup(x => x.Get(_gameId)).Returns(InitializeGame(GameStatusEnum.ONGOING));

            GameGuessResult response = await _hiLoGameService.Guess(_gameId, guess);

            Assert.AreEqual(expectedResult, response.GameGuessResultEnum);
            _gameRepositoryMock.Verify(x => x.Update(It.IsAny<GameDto>()), Times.Once);
        }

        [TestMethod]
        public async Task GuessInvalidNumber()
        {
            _gameRepositoryMock.Setup(x => x.Get(_gameId)).Returns(InitializeGame(GameStatusEnum.ONGOING));
            Guid gameId = Guid.Parse("b9442a81-587b-4dac-8ee4-23412bb1c2c6");

            await Assert.ThrowsExceptionAsync<GuessNotInRangeException>(async () => await _hiLoGameService.Guess(gameId, -2));
        }

        [TestMethod]
        public async Task GuessOnAlreadyFinishedGame()
        {
            _gameRepositoryMock.Setup(x => x.Get(_gameId)).Returns(InitializeGame(GameStatusEnum.FINISHED));
            Guid gameId = Guid.Parse("b9442a81-587b-4dac-8ee4-23412bb1c2c6");

            await Assert.ThrowsExceptionAsync<GameClosedException>(async () => await _hiLoGameService.Guess(gameId, 2));
        }
    }
}