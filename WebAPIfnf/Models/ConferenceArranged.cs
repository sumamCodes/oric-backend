using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class ConferenceArranged
    {
        [Key]
        public int id { get; set; }


        [ForeignKey("ric_form_2_id")]

        public required ric_form_2 ric_form_2 { get; set; }
        public string? eventtype { get; set; } // Training, Workshop, Seminar, Conference, Other

        [Required]
        public string? eventlevel { get; set; } // National or International

        [Required]
        public string? title { get; set; } // Title of Training

        [Required]
        public DateTime? eventdate { get; set; } // Date of Event

        [Required]
        public int? numberofparticipants { get; set; } // No. of Participants

        [Required]
        public string? focusandoutcomes { get; set; } // Major Focus Area & Outcomes

        [Required]
        public string? organizer { get; set; } // Organizer of the event

        [Required]
        public string? audiencetype { get; set; } // Student, Faculty, Researchers

        public byte[]? evidence { get; set; }
    }
}
