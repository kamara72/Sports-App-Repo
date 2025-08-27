using NationalCountyMeet.Web.Models.Others;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NationalCountyMeet.Web.Models
{
    public class TournamentOfficial : UserActivities
    {
        [Key]
        public int TournamentOfficialId { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Middle Name")]
        public string? MiddleName { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {MiddleName} {LastName}";

        [Required]
        [Display(Name = "Contact/Phone")]
        public string Contact { get; set; }

        public string? Email { get; set; }

        public Gender Gender { get; set; }

        [Required]
        [Display(Name = "Place Of Birth")]
        public string PlaceOfBirth { get; set; }

        // [Required]
        [Display(Name = "Team")]
        public int CountyId { get; set; }

        [Required]
        [Display(Name = "Ethinic Group")]
        public Ethnicity Ethnicity { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Official")]
        [Required]
        public string TeamOfficial { get; set; }

        [Display(Name = "Tournament Official Photo")]
        [NotMapped]
        public IFormFile? TournamentOfficialPhoto { get; set; }

        public string? TournamentOfficialPhotoUrl { get; set; }


        // Navigation properties
        public County County { get; set; }
    }
}
