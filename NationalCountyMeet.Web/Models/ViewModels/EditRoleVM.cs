using System.ComponentModel.DataAnnotations;

namespace NationalCountyMeet.Web.Models.ViewModels
{
    public class EditRoleVM
    {
        public EditRoleVM()
        {
            Users = new List<string>();
        }
        public string RoleId { get; set; }

        [Display(Name = "Role Name")]
        [Required(ErrorMessage = "Required*")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; }
    }
}
