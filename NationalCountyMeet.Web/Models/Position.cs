using System.ComponentModel.DataAnnotations;

namespace NationalCountyMeet.Web.Models
{
    public class Position
    {
        [Key]
        public int PositionId { get; set; }

        [Required]  
        [StringLength(50)]
        [Display(Name = "Position Name")]
        public string PositionName { get; set; }

        public string? Description { get; set; }
    }
}
