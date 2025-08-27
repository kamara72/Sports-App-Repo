using System.ComponentModel.DataAnnotations;

namespace NationalCountyMeet.Web.Models.ViewModels
{
    public class PlayerDocumentVM
    {
        [Display(Name = "Player ID")]
        public int PlayerId { get; set; }

        [Required(ErrorMessage = "Required*")]
        public List<IFormFile> Files { get; set; }
        public string? Description { get; set; }

        [Display(Name = "Document Type")]
        [Required(ErrorMessage = "Required*")]
        public string DocumentType { get; set; }
    }
}
