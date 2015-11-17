namespace TicTacToe.Data
{
    using TicTacToe.Data.Repositories;
    using Models;

    public interface ITicTacToeData
    {
        IRepository<ApplicationUser> Users { get; }

        IRepository<Game> Games { get; }

        int SaveChanges();
    }
}
