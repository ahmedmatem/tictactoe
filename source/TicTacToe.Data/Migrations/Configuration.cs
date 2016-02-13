namespace TicTacToe.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TicTacToe.Data.TicTacToeDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            // TODO: set this.AutomaticMigrationDataLossAllowed to false
            this.AutomaticMigrationDataLossAllowed = true;
        }
    }
}
