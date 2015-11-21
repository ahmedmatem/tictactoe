namespace TicTacToe.Web.Controllers
{
    using System.Web.Http;

    using Data;

    [Authorize]
    public abstract class BaseController : ApiController
    {
        protected ITicTacToeData data;

        protected BaseController(ITicTacToeData data)
        {
            this.data = data;
        }
    }
}