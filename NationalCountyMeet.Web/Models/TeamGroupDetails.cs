using System.ComponentModel.DataAnnotations;

namespace NationalCountyMeet.Web.Models
{
    public class TeamGroupDetails
    {
        [Key]
        public int TeamGroupDetailsId { get; set; }

        [Display(Name = "County")]
        public string CountyId { get; set; }

        [Display(Name = "Team Group")]
        public string TeamGroupId { get; set; }
    }
}
