using System.ComponentModel.DataAnnotations;

namespace NationalCountyMeet.Web.Models.ViewModels
{
    public class PlayerStatsIndexVM
    {
        [Key]
        public int PlayerStatisticId { get; set; }

        [Required]
        [Display(Name = "Player")]
        public int PlayerId { get; set; }

        [Display(Name = "Full Name")]
        public string? FullName { get; set; }

        [Required]
        [Display(Name = "Match")]
        public int MatchId { get; set; }

        public int CountyId { get; set; }

        [Required]
        [Display(Name = "Home Team")]
        public int HomeTeamId { get; set; }

        [Required]
        [Display(Name = "Away Team")]
        public int AwayTeamId { get; set; }

        [Display(Name = "Fixture")]
        public string MatchFixture => $"{HomeTeamId} vs {AwayTeamId}";
    }
}
