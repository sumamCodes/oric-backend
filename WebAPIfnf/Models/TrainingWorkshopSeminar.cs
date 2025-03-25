using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class TrainingWorkshopSeminar
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("ric_form_2_id")]

        public required ric_form_2 ric_form_2 { get; set; }
        public string? eventtype { get; set; } // Training, Workshop, Seminar, Conference, Other

        public string? eventlevel { get; set; } // National or International

        public string? title { get; set; } // Title of Event

        public DateTime eventdate { get; set; } // Date of Event

        public int numberofparticipants { get; set; } // No. of Participants

        public string? focusandoutcomes { get; set; } // Major Focus Area & Outcomes

        public byte[]? evidence { get; set; }// Path for uploaded file (Event Certificate/Photograph)
    }

}
