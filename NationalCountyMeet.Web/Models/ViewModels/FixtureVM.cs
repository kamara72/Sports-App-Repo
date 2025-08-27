using System.ComponentModel.DataAnnotations;

namespace NationalCountyMeet.Web.Models.ViewModels
{
    public class FixtureVM
    {
        public int FixtureId { get; set; }

        [Display(Name = "Match Date")]
        public DateTime MatchDate { get; set; }

        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [Display(Name = "Home Team")]
        public int HomeId { get; set; }
        public string? HomeTeamName { get; set; }

        [Display(Name = "Home Team")]
        public int AwayId { get; set; }
        public string? AwayTeamName { get; set; }

        [Display(Name = "Center Referee")]
        public int CenterOfficialId { get; set; }
        public string? CenterOfficialName { get; set; }

        [Display(Name = "Match Venue")]
        public int VenueId { get; set; }
        public string? VenueName { get; set; }

        [Display(Name = "First Lines Man")]
        public int FirstLinesmanId { get; set; }
        public string? FirstLinesmaneName { get; set; }

        [Display(Name = "Second Lines Man")]
        public int SecondLinesmanId { get; set; }
        public string? SecondLinesmanName { get; set; }

        [Display(Name = "Fourth Official")]
        public int FourthOfficialId { get; set; }
        public string? FourthOfficialName { get; set; }

        [Display(Name = "Round")]
        public int? RoundId { get; set; }

        public string? RoundName { get; set; }

        public string VenuePhotoUrl { get; set; }
    }
}
