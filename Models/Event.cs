using System;
using System.ComponentModel.DataAnnotations;

namespace CTRL.Models
{
    // Represents an event record stored in the CTRL platform database
    public class Event
    {
        // Primary key for the event record
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        // Stores the event title displayed in event listings and detail pages
        public string Title { get; set; } = string.Empty;

        [Required]
        // Stores the scheduled date of the event
        public DateTime Date { get; set; }

        [Required]
        [StringLength(150)]
        // Stores the event venue or location
        public string Venue { get; set; } = string.Empty;

        [StringLength(100)]
        // Stores the event category for filtering and search
        public string Category { get; set; } = string.Empty;

        [StringLength(500)]
        // Stores descriptive information about the event
        public string Description { get; set; } = string.Empty;

        // Optional ticket price associated with the event
        public decimal? TicketPrice { get; set; }

        [StringLength(250)]
        // Stores the image path used for event thumbnails and display
        public string ImageUrl { get; set; } = string.Empty;

        // Foreign key linking an event to an associated DJ profile
        public int? DJId { get; set; }

        // Navigation property used to access the related DJ record
        public DJ? DJ { get; set; }
    }
}
