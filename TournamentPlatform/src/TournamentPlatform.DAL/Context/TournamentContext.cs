using EntityFramework.Exceptions.SqlServer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TournamentPlatform.DL.Domain.BusinessDomains;
using TournamentPlatform.DL.Domain.IdentityDomains;

namespace TournamentPlatform.DAL.Context
{
    public sealed class TournamentContext : IdentityDbContext<ApplicationUser>
    {
        public TournamentContext(DbContextOptions<TournamentContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamInfo> TeamInfo { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<PlayerTeam> PlayerTeams { get; set; }
        public DbSet<TournamentTeam> TournamentTeams { get; set; }
        public DbSet<TeamInvite> TeamInvites { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TeamInfo>()
                   .HasOne(teamInfo => teamInfo.Team)
                   .WithMany(team => team.TeamInfo)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Match>()
                   .HasOne(match => match.NextMatch)
                   .WithMany(match => match.PrevMatches)
                   .OnDelete(DeleteBehavior.NoAction);

            //builder.Entity<PlayerTeam>()
            //       .HasOne(playerTeam =>playerTeam.Team)
            //       .WithMany(team => team.PlayersTeams)
            //       .OnDelete(DeleteBehavior.NoAction);

            //builder.Entity<TeamInvite>()
            //       .HasOne(teamInvite =>teamInvite.Team)
            //       .WithMany(team => team.TeamInvites)
            //       .OnDelete(DeleteBehavior.NoAction);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseExceptionProcessor();
        }
    }
}