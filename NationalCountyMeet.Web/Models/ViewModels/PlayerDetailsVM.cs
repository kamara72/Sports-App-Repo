using NationalCountyMeet.Web.Models.Others;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Primitives;

namespace NationalCountyMeet.Web.Models.ViewModels
{
    public class PlayerDetailsVM
    {
        public int PlayerID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string? MiddleName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Contact/Phone")]
        public string Contact { get; set; }

        public string? Email { get; set; }

        public Gender Gender { get; set; }

        [Display(Name = "Place Of Birth")]
        public string PlaceOfBirth { get; set; }

        [Display(Name = "County Of Origin")]
        public int CountyOfOriginCountyId { get; set; }

        public string CountyOfOriginCounty { get; set; }

        [Display(Name = "Ethinic Group")]
        public Ethnicity Ethnicity { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Player's County")]
        public int CountyId { get; set; }

        public string? CountyName { get; set; }

        [Display(Name = "Jersey Number")]
        public int? JerseyNumber { get; set; }

        [Display(Name = "Player Position")]
        public string? PlayerPosition { get; set; }

        [Display(Name = "Home Address")]
        public string? HomeAddress { get; set; }

        [Display(Name = "Home Contact")]
        public string? HomeContact { get; set; }

        public string? PlayerPhotoUrl { get; set; }

        //public int PlayerStatisticsId { get; set; }
        //public int PlayerGoals { get; set; }

        public int DocumentId { get; set; }
        public List<PlayerDocument> PlayerDocument { get; set; }
        public int PlayerStatisticsId { get; set; }
        public List<PlayerStatistic> PlayerStatistics { get; set; }
    }
}
