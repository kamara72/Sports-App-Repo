using System.ComponentModel.DataAnnotations;

namespace NationalCountyMeet.Web.Models
{
    public class PlayerStatistic
    {
        [Key]
        public int PlayerStatisticId { get; set; }

        [Required]
        [Display(Name = "Player")]
        public int PlayerId { get; set; }

        [Required]
        [Display(Name = "Match")]
        public int MatchId { get; set; }

        [Display(Name = "Goal Score")]
        public int? Goals { get; set; }
        public int? Asists { get; set; }

        [Display(Name = "Minutes Played")]
        public int? MinutesPlayed { get; set; }

        // Navigations
        public Player Player { get; set; }
        public Match Match { get; set; }

    }
}
