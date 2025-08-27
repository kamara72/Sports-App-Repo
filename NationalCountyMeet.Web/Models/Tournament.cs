using NationalCountyMeet.Web.Models.Others;
using System.ComponentModel.DataAnnotations;

namespace NationalCountyMeet.Web.Models
{
    public class Tournament : UserActivities
    {
        [Key]
        public int TournamentId { get; set; }

        // Get only year from the calendar
        [Display(Name = "Year")]
        [DataType(DataType.Date)]
        public DateTime TournamentYear { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public string? Note { get; set; }
    }
}
