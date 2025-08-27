using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace NationalCountyMeet.Web.Models.Others
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(100)]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Fullname")]
        public string FullName => $"{FirstName} {MiddleName} {LastName}";

        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public string HomeAddress { get; set; }

        public int Age => CalculateAge(DateOfBirth);

        public int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
