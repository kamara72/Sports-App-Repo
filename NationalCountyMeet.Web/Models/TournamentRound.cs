using NationalCountyMeet.Web.Models.Others;
using System.ComponentModel.DataAnnotations;

namespace NationalCountyMeet.Web.Models
{
    public class TournamentRound : UserActivities
    {
        [Key]
        public int TournamentRoundId { get; set; }

        [Required]
        public string Rounds { get; set; }

        public string Notes { get; set; }
    }
}
