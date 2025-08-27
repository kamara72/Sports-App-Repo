using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NationalCountyMeet.Web.Models;
using NationalCountyMeet.Web.Models.Others;

namespace NationalCountyMeet.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<Employee>()
            //    .ToTable(name: "Employees", empTable => empTable.IsTemporal());

            //builder.Entity<Student>()
            //    .ToTable(name: "Students", studTeable => studTeable.IsTemporal());

            builder.Entity<Player>().HasQueryFilter(p => !p.IsDeleted);

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.Entity<MatchLineup>()
                .HasOne(f => f.Player)
                .WithMany()
                .HasForeignKey(f => f.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);
        }


        public DbSet<Player> Players { get; set; }
        public DbSet<County> Counties { get; set; }
        public DbSet<MatchVenue> Venues { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<TournamentGroup> TournamentGroups { get; set; }
        public DbSet<TournamentRound> TournamentRounds { get; set; }
        public DbSet<TeamGroup> TeamGroups { get; set; }
        public DbSet<Fixture> Fixtures { get; set; }
        public DbSet<MatchOfficial> MatchOfficials { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }
        public DbSet<MatchLineup> MatchLineups { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<TournamentOfficial> TournamentOfficials { get; set; }
        public DbSet<TeamOfficial> TeamOfficials { get; set; }
        public DbSet<PlayerDocument> PlayerDocuments { get; set; }
        public DbSet<TeamGroupDetails> TeamGroupDetails { get; set; }
    }
}
