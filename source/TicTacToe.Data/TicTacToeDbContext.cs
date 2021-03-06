﻿namespace TicTacToe.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Models;
    using Migrations;

    public class TicTacToeDbContext : IdentityDbContext<ApplicationUser>
    {
        public TicTacToeDbContext()
            : base("TicTacToeConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TicTacToeDbContext, Configuration>());
        }

        public static TicTacToeDbContext Create()
        {
            return new TicTacToeDbContext();
        }

        public virtual IDbSet<Game> Games { get; set; }

        public virtual IDbSet<Token> Tokens { get; set; }
    }
}
