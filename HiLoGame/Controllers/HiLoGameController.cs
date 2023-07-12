using HiLoGame.Model;
using HiLoGame.Model.Exceptions;
using HiLoGame.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace HiLoGame.Controllers
{
    [ApiController]
    [Route("[hilo]")]
    public class HiLoGameController : ControllerBase
    {

        private readonly IHiLoGameService _hiLoGameService;
        private readonly IStringLocalizer<HiLoGameController> _localizer;

        public HiLoGameController(IHiLoGameService hiLoGameService, IStringLocalizer<HiLoGameController> localizer)
        {
            _hiLoGameService = hiLoGameService;
            _localizer = localizer;
        }

        [Route("/new")]
        [HttpPost]
        public async Task<ActionResult<String>> CreateNewGame()
        {
            var gameId = await _hiLoGameService.CreateGame();
            return Ok(gameId.ToString());
        }

        [Route("/guess")]
        [HttpPost]
        public async Task<ActionResult<String>> Guess(Guid gameId, int guess)
        {
            try
            {
                GameGuessResult result = await _hiLoGameService.Guess(gameId, guess);
                String response = null;
                switch(result.GameGuessResultEnum)
                {
                    case GameGuessResultTypeEnum.HI:
                        response = _localizer["guessResult.hi"].Value;
                        break;
                    case GameGuessResultTypeEnum.OK:
                        response = String.Format(_localizer["guessResult.ok"].Value, result.NumberOfTry);
                        break;
                    case GameGuessResultTypeEnum.LO:
                        response = _localizer["guessResult.lo"].Value;
                        break;
                }
                return Ok(response);
            } catch(GameNotFoundException ex)
            {
                return NotFound(String.Format(_localizer[ex.ResourceKey].Value));
            }
            catch(TranslatableException ex)
            {
                return BadRequest(String.Format(_localizer[ex.ResourceKey].Value, ex.MsgParams));
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}