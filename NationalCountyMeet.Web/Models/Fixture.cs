using NationalCountyMeet.Web.Models.Others;
using System.ComponentModel.DataAnnotations;

namespace NationalCountyMeet.Web.Models
{
    public class Fixture : UserActivities
    {
        [Key]
        public int FixtureId { get; set; }

        [Required]
        [Display(Name = "Match Time")]
        [DataType(DataType.Date)]
        public DateTime MatchDate { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        [DataType(DataType.Date)]
        public DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "Center Referee")]
        public int CenterOfficialId { get; set; }

        [Required]
        [Display(Name = "Lines man One")]
        public int FirstLinesManId { get; set; }

        [Required]
        [Display(Name = "Lines man Two")]
        public int SecondLinesManId { get; set; }

        [Required]
        [Display(Name = "Fourth Official")]
        public int FourthOfficial { get; set; }

        [Required]
        [Display(Name = "Home Team")]
        public int HomeTeamId { get; set; }

        [Required]
        [Display(Name = "Away Team")]
        public int AwayTeamId { get; set; }

        [Display(Name = "Fixture")]
        public string MatchFixture => $"{HomeTeamId} vs {AwayTeamId}";

        [Required]
        [Display(Name = "Venue")]
        public int MatchVenueId { get; set; }
        
        [Display(Name = "Round")]
        public int? TournamentRoundId { get; set; }


        // Navigation
        public TournamentRound TournamentRound { get; set; }
        public MatchVenue MatchVenue { get; set; }
    }
}
