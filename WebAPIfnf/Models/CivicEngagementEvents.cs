using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class CivicEngagementEvents
    {
        public int id { get; set; }
        public int ric_form_1_id { get; set; }  // Foreign key to RicForm1

        public string? event_title { get; set; }
        public DateTime? event_date { get; set; }
        public string? community_component_involved { get; set; }
        public string? outcome { get; set; } // Case study, policy advice, etc.
        public string? collaboration_developed { get; set; } // Local authorities, government departments, etc.
        public string? engaged_CSOs_Or_NGOs { get; set; }
        public string? sponsoring_agency { get; set; }
        public decimal? grant_value { get; set; } // Optional
        public string? arranged_or_participated { get; set; } // Will be Arranged / Participated
        public string? dissemination_material { get; set; } // Brochure, report, web link, etc.
        public string? remarks { get; set; }
        public byte[]? evidence { get; set; } // Attachment

        // Navigation property
        [ForeignKey("ric_form_1_id")]
        public required ric_form_1 ric_form_1 { get; set; }
    }

}
