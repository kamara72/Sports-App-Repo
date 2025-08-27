using NationalCountyMeet.Web.Models.Others;
using System.ComponentModel.DataAnnotations;

namespace NationalCountyMeet.Web.Models
{
    public class PlayerStatistic : UserActivities
    {
        [Key]
        public int PlayerStatisticId { get; set; }

        [Required]
        [Display(Name = "Player")]
        public int PlayerId { get; set; }

        [Required]
        [Display(Name = "Match")]
        public int MatchId { get; set; }

        [Display(Name = "Saves")]
        public int? Saves { get; set; }

        [Display(Name = "Minutes Played")]
        public int? MinutesPlayed { get; set; }

        [Display(Name = "Passes Completed")]
        public int? PassesCompleted { get; set; }

        public int? Goals { get; set; }

        public int? Asists { get; set; }

        public int? Shots { get; set; }

        [Display(Name = "Shots On Target")]
        public int? ShotsOnTarget { get; set; }

        public int? Tackles { get; set; }

        [Display(Name = "Red Card")]
        public int? RedCards { get; set; }

        [Display(Name = "Yellow Card")]
        public int? YellowCards { get; set; }

        //[Display(Name = "County")]
        //public int CountyId { get; set; }

        // Navigations
        public Player Player { get; set; }
        public Match Match { get; set; }
        // public County County { get; set; }
    }
}
