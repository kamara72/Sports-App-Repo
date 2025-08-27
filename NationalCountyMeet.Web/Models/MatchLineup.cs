using NationalCountyMeet.Web.Models.Others;
using System.ComponentModel.DataAnnotations;

namespace NationalCountyMeet.Web.Models
{
    public class MatchLineup : UserActivities
    {
        [Key]
        public int MatchLineupId { get; set; }

        [Display(Name = "Player")]
        public int PlayerId { get; set; }

        [Display(Name = "Match")]
        public int MatchId { get; set; }

        [Display(Name = "County")]
        public int CountyId { get; set; }

        [Display(Name = "Is Starting?")]
        public bool IsStarting { get; set; }

        [Display(Name = "Minute Substituted In")]
        public int MinuteSubstitutedIn { get; set; }

        [Display(Name = "Minute Substituted Out")]
        public int MinuteSubstitutedOut { get; set; }

        // Navigation 
        public County County { get; set; }
        public Player Player { get; set; }
        public Match Match { get; set; }
    }
}
