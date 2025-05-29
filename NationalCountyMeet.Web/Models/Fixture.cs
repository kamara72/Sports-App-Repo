using System.ComponentModel.DataAnnotations;

namespace NationalCountyMeet.Web.Models
{
    public class Fixture
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
        [Display(Name = "Referee")]
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

        [Required]
        [Display(Name = "Venue")]
        public int MatchVenueId { get; set; }

        // Navigation
        public MatchVenue MatchVenue { get; set; }
    }
}
