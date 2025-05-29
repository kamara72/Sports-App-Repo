using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NationalCountyMeet.Web.Models;

namespace NationalCountyMeet.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
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

            //foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            //{
            //    relationship.DeleteBehavior = DeleteBehavior.Restrict;
            //}

            //builder.Entity<MatchVenue>()
            //    .HasOne(f => f.County)
            //    .WithMany()
            //    .HasForeignKey(f => f.CountyId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }


        public DbSet<Player> Players { get; set; }
        public DbSet<County> Counties { get; set; }
        public DbSet<MatchVenue> Venues { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<TournamentGroup> TournamentGroups { get; set; }
        public DbSet<TournamentRound> TournamentRounds { get; set; }
        public DbSet<TeamGroup> TeamGroups { get; set; }
        public DbSet<MatchOfficial> MatchOfficials { get; set; }
        public DbSet<Fixture> Fixtures { get; set; }
    }
}
