using System.ComponentModel.DataAnnotations;

namespace NationalCountyMeet.Web.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Passsword { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
