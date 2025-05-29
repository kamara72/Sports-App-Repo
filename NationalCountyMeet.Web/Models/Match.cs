using System.ComponentModel.DataAnnotations;

namespace NationalCountyMeet.Web.Models
{
    public class Match
    {
        [Key]
        public int MatchId { get; set; }

        [Required]
        [Display(Name = "Team One Score")]
        public int TeamOneScore { get; set; }

        [Required]
        [Display(Name = "Team Two Score")]
        public int TeamTwoScore { get; set; }

        public string Result => $"{TeamOneScore} - {TeamTwoScore}";

        [Display(Name = "Match Venue")]
        public int MatchVenueId { get; set; }

        //[Required]
        //[Display(Name = "Match Date")]
        //public DateTime MatchDate { get; set; }

        [Display(Name = "Round")]
        public int TournamentRoundId { get; set; }
         
        public string? Notes { get; set; }

        public MatchVenue MatchVenue { get; set; }
        public TournamentRound TournamentRound { get; set; }
    }
}
