using NuGet.Protocol.Core.Types;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NationalCountyMeet.Web.Models
{
    public class County
    {
        [Key]
        public int CountyId { get; set; }

        [Required]
        [Display(Name = "County Name")]
        public string CountyName { get; set; }

        [Required]
        [Display(Name = "Capital City")]
        public string CountyCapital { get; set; }

        [Required]
        [Display(Name = "County Region")]
        public string CountyRegion { get; set; }

        [Required]
        [Display(Name = "Year Established")]
        [DataType(DataType.Date)]
        public DateTime YearEstablished { get; set; }

        [Display(Name = "Player Photo")]
        [NotMapped]
        public IFormFile? CountyFlagPhoto { get; set; }

        public string? CountyFlagPhotoUrl { get; set; }
        public List<Player>? Players { get; }
        public List<TeamOfficial>? TeamOfficial { get; set; }
    }
}
