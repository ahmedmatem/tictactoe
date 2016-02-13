namespace TicTacToe.Web.Controllers
{
    using System;
    using System.Web.Http;
    using System.Linq;
    using System.Text;

    using Microsoft.AspNet.Identity;
    using AutoMapper.QueryableExtensions;

    using GameLogic;

    using Data;
    using TicTacToe.Models;
    using DataModels;
    using System.IO;

    public class GamesController : BaseController
    {
        private IGameResultValidator resultValidator;

        public GamesController() 
            : this(new TicTacToeData(new TicTacToeDbContext()), new GameResultValidator())
        {

        }

        public GamesController(ITicTacToeData data, IGameResultValidator resultValidator)
            : base(data)
        {
            this.resultValidator = resultValidator;
        }

        [HttpPost]
        public IHttpActionResult Create()
        {
            var currentUserId = this.User.Identity.GetUserId();

            var newGame = new Game()
            {
                FirstPlayerId = currentUserId,
            };

            this.data.Games.Add(newGame);
            this.data.SaveChanges();

            return this.Ok(newGame.Id);
        }

        [HttpPost]
        public IHttpActionResult Join()
        {
            var currentUserId = this.User.Identity.GetUserId();

            var game = this.data.Games
                .All()
                .Where(g => g.State == GameState.WaitingForSecondPlayer && g.FirstPlayerId != currentUserId)
                .FirstOrDefault();

            if (game == null)
            {
                return NotFound();
            }

            game.SecondPlayerId = currentUserId;
            game.State = GameState.TurnX;
            this.data.SaveChanges();            

            return Ok(game.Id);
        }

        [HttpGet]
        public IHttpActionResult Status(string gameID)
        {
            var currentUserId = this.User.Identity.GetUserId();
            var gameIdAsGuid = new Guid(gameID);

            var game = this.data.Games.All()
                        .Where(g => g.Id == gameIdAsGuid)
                        .Select(g => new { g.FirstPlayerId, g.SecondPlayerId })
                        .FirstOrDefault();

            if(game == null)
            {
                return NotFound();
            }

            if(game.FirstPlayerId != currentUserId && game.SecondPlayerId != currentUserId)
            {
                return this.BadRequest("This is not your game.");
            }

            var gameInfo = this.data.Games
                .All()
                .Where(g => g.Id == gameIdAsGuid)
                .ProjectTo<GameInfoDataModel>()
                .FirstOrDefault();

            return this.Ok(gameInfo);
        }

        [HttpPost]
        public IHttpActionResult Play(PlayRequestDataModel request)
        {
            var currentUserId = this.User.Identity.GetUserId();

            if (request == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gameIdAsGuid = new Guid(request.GameId);

            var game = this.data.Games.Find(gameIdAsGuid);

            if(game == null)
            {
                return this.BadRequest("Invalida game id!");
            }

            if(game.State == GameState.Draw ||
                game.State == GameState.FirstPlayerWon ||
                game.State == GameState.SecondPlayerWon)
            {
                return BadRequest("The game has ended!");
            }

            if(game.State != GameState.TurnO && game.State != GameState.TurnX)
            {
                return this.BadRequest("Invalid game state!");
            }

            if(game.FirstPlayerId != currentUserId && game.SecondPlayerId != currentUserId)
            {
                return this.BadRequest("This is not your game!");
            }

            if((game.State == GameState.TurnX && currentUserId != game.FirstPlayerId) ||
                game.State == GameState.TurnO && currentUserId != game.SecondPlayerId)
            {
                return this.BadRequest("It is not your turn!");
            }

            var positionIndex = (request.Row - 1) * 3 + request.Col - 1;
            if(game.Board[positionIndex] != '-')
            {
                return this.BadRequest("Invalid position!");
            }

            var gameBoardAsStringBuilder = new StringBuilder(game.Board);
            gameBoardAsStringBuilder[positionIndex] =
                game.State == GameState.TurnO ? 'O' : 'X';
            game.Board = gameBoardAsStringBuilder.ToString();

            game.State = (game.State == GameState.TurnO ? GameState.TurnX : GameState.TurnO);

            this.data.SaveChanges();

            var gameResult = resultValidator.GetResult(game.Board);
            switch (gameResult)
            {
                case GameResult.NotFinished:
                    break;
                case GameResult.WonByO:
                    game.State = GameState.SecondPlayerWon;
                    this.data.SaveChanges();
                    break;
                case GameResult.WonByX:
                    game.State = GameState.FirstPlayerWon;
                    this.data.SaveChanges();
                    break;
                case GameResult.Draw:
                    game.State = GameState.Draw;
                    this.data.SaveChanges();
                    break;
                default:
                    break;
            }

            if(game.State == GameState.Draw)
            {
                return this.Ok("Draw!");
            }

            if (game.State == GameState.FirstPlayerWon)
            {
                return this.Ok("First player won!");
            }
            if (game.State == GameState.SecondPlayerWon)
            {
                return this.Ok("Second player won!");
            }

            return this.Ok();
        }
    }
}