using Microsoft.VisualStudio.TestTools.UnitTesting;
using HiLoGame.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using HiLoGame.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace HiLoGame.Controllers.Tests
{
    [TestClass()]
    public class HiLoGameControllerTests
    {
        private Mock<IHiLoGameService> _hiLoGameServiceMock;
        private Mock<IStringLocalizer<HiLoGameController>> _localizer;
        private HiLoGameController _hiLoGameController;

        private readonly Guid _gameId = Guid.Parse("b9442a81-587b-4dac-8ee4-23412bb1c2c6");

        [TestInitialize]
        public void initialize()
        {
            _hiLoGameServiceMock = new Mock<IHiLoGameService>();
            _localizer = new Mock<IStringLocalizer<HiLoGameController>>();
            _hiLoGameController = new HiLoGameController(_hiLoGameServiceMock.Object, _localizer.Object);

            _localizer.Setup(x => x[It.IsAny<string>()]).Returns(new LocalizedString("key", "value"));
        }

        [TestMethod()]
        public async Task CreateNewGameTest()
        {
            _hiLoGameServiceMock.Setup(x => x.CreateGame()).ReturnsAsync(_gameId);
            ActionResult<string> actionResult = await _hiLoGameController.CreateNewGame();

            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));
            var result = (OkObjectResult)actionResult.Result;
            Assert.IsNotNull(result.Value);
            Assert.AreEqual<String>(_gameId.ToString(), result.Value.ToString());
        }

        [TestMethod()]
        public async Task GuessTest()
        {
            _hiLoGameServiceMock.Setup(x => x.Guess(_gameId, 12)).ReturnsAsync(new Model.GameGuessResult { GameGuessResultEnum = Model.GameGuessResultTypeEnum.HI});
            ActionResult<string> actionResult = await _hiLoGameController.Guess(_gameId, 12);

            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));
            var result = (OkObjectResult)actionResult.Result;
            Assert.IsNotNull(result.Value);
            Assert.AreEqual<String>("value", result.Value.ToString());
        }
    }
}