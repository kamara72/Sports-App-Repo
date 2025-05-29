using System.ComponentModel.DataAnnotations;

namespace NationalCountyMeet.Web.Models
{
    public class TeamGroup
    {
        [Key]
        public int TeamGroupId { get; set; }

        [Display(Name = "Tournament Group")]
        public int TournamentGroupId { get; set; }

        [Display(Name = "Tournament Year")]
        public int TournamentId { get; set; }

        [Display(Name = "County")]
        public int CountyId { get; set; }

        public string? Note { get; set; }

        public Tournament Tournament { get; set; }
        public TournamentGroup TournamentGroup { get; set; }
        public County County { get; set; }
    }
}
