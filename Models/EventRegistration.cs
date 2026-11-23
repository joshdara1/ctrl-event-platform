using System;
using System.ComponentModel.DataAnnotations;

namespace CTRL.Models
{
    // Represents an attendee registration linking a user to an event
    public class EventRegistration
    {
        // Primary key for the event registration record
        public int Id { get; set; }

        // Stores the identifier of the registered user attending the event
        [Required]
        public string UserId { get; set; } = string.Empty;

        // Foreign key linking the registration to a specific event
        public int EventId { get; set; }

        // Navigation property used to access the related event record
        public Event? Event { get; set; }

        // Stores the date and time the attendee registration was created
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}