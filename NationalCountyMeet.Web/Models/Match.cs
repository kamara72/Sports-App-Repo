using NationalCountyMeet.Web.Models.Others;
using System.ComponentModel.DataAnnotations;

namespace NationalCountyMeet.Web.Models
{
    public class Match : UserActivities
    {
        [Key]
        public int MatchId { get; set; }

        [Required]
        [Display(Name = "Home Team Score")]
        public int HomeTeamScore { get; set; }

        [Required]
        [Display(Name = "Away Team Score")]
        public int AwayTeamScore { get; set; }

        [Display(Name = "Match")]
        public string MatchFixture => $"{HomeTeamScore} vs {AwayTeamScore}";

        public string Result => $"{HomeTeamScore} - {AwayTeamScore}";

        [Display(Name = "Match Venue")]
        public int MatchVenueId { get; set; }

        //[Display(Name = "Round")]
        //public int TournamentRoundId { get; set; }

        [Display(Name = "Fixture")]
        public int FixtureId { get; set; }
         
        public string? Notes { get; set; }

        public Fixture Fixture { get; set; }
        public MatchVenue MatchVenue { get; set; }
        // public TournamentRound TournamentRound { get; set; }
    }
}
