using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NationalCountyMeet.Web.Models
{
    public class MatchVenue
    {
        [Key]
        public int MatchVenueId { get; set; }

        [Required]
        [Display(Name = "Venue Name")]
        public string VenueName { get; set; }

        [Required]
        [Display(Name = "Venue Location")]
        public string Location { get; set; }

        [Display(Name = "Total Seats")]
        public int? Capacity { get; set; }

        [Required]
        [Display(Name = "County")]
        public int CountyId { get; set; }

        [Display(Name = "Venue Photo")]
        [NotMapped]
        public IFormFile? VenuePhoto { get; set; }

        public string? VenuePhotoUrl { get; set; }

        // Navigation
        // public County County { get; set; }
    }
}
