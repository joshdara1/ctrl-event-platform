using System.ComponentModel.DataAnnotations;

namespace CTRL.Models
{
    // Represents a DJ profile stored in the CTRL platform database
    public class DJ
    {
        // Primary key for the DJ profile record
        public int Id { get; set; }

        // Stores the DJ or performer name displayed in profile listings
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        // Stores the DJ's music genre or performance style
        [StringLength(100)]
        public string Genre { get; set; } = string.Empty;

        // Stores profile biography information for the DJ
        [StringLength(500)]
        public string Bio { get; set; } = string.Empty;

        // Stores the DJ's Instagram profile link
        [StringLength(200)]
        public string Instagram { get; set; } = string.Empty;

        // Stores the DJ's SoundCloud profile link
        [StringLength(200)]
        public string SoundCloud { get; set; } = string.Empty;

        // Stores the image path used for DJ profile pictures
        [StringLength(250)]
        public string ImageUrl { get; set; } = string.Empty;
    }
}