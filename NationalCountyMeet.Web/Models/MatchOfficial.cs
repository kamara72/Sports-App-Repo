using NationalCountyMeet.Web.Models.Others;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NationalCountyMeet.Web.Models
{
    public class MatchOfficial
    {
        public int MatchOfficialId { get; set; }
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

        [Display(Name = "Official Status")]
        public MatchOfficialStatus MatchOfficialStatus { get; set; }

        [Required]
        [Display(Name = "Place Of Birth")]
        public string PlaceOfBirth { get; set; }

       // [Required]
        [Display(Name = "County Of Origin")]
        public int CountyId { get; set; }

        [Required]
        [Display(Name = "Ethinic Group")]
        public Ethnicity Ethnicity { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Match Official Photo")]
        [NotMapped]
        public IFormFile? MatchOfficialPhoto { get; set; }

        public string? MatchOfficialPhotoUrl { get; set; }

        public County County { get; set; }
    }
}
