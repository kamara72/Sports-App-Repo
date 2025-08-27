using System.ComponentModel.DataAnnotations;

namespace NationalCountyMeet.Web.Models
{
    public class PlayerDocument
    {
        [Key]
        public int DocumentId { get; set; }

        [Display(Name = "Player")]
        public int PlayerId { get; set; }

        [Required]
        [Display(Name = "Document Name")]
        public string DocumentName { get; set; }

        public string? Description { get; set; }

        public string FilePath { get; set; }

        [Required]
        [Display(Name = "Document Type")]
        public string DocumentType { get; set; }

        // Navigation Property
        public Player Player { get; set; }
    }
}
