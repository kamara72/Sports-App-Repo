using NationalCountyMeet.Web.Models.Others;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NationalCountyMeet.Web.Models
{
    public class Player
    {
        [Key]
        public int PlayerId { get; set; }
                
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

        //[Required]
        //[Display(Name = "County Of Birth")]
        //public int CountyOfBirthId { get; set; }

        [Required]
        [Display(Name = "County Of Origin")]
        public int CountyId { get; set; }

        [Required]
        [Display(Name = "Ethinic Group")]
        public Ethnicity Ethnicity { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Player's County")]
        public int PlayerCountyID { get; set; }

        [Display(Name = "Jersey Number")]
        public int? JerseyNumber { get; set; }

        [Display(Name = "Player Position")]
        public string? PlayerPosition { get; set; }

        [Display(Name = "Registration Date")]
        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        [Display(Name = "Player Photo")]
        [NotMapped]
        public IFormFile? PlayerPhoto { get; set; }

        public string? PlayerPhotoUrl { get; set; }



        // Navigation property
        public County County { get; set; }
    }
}
