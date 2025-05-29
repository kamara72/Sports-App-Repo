using System.ComponentModel.DataAnnotations;

namespace NationalCountyMeet.Web.Models
{
    public class TournamentGroup
    {
        [Key]
        public int TournamentGroupId { get; set; }

        [Required]
        [Display(Name = "Group Name")]
        public string GroupName { get; set; }

        [Required]
        [Display(Name = "Group Alias")]
        public string GroupAlias { get; set; }

        public string? Note { get; set; }
    }
}
