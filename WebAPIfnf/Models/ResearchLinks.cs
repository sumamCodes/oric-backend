using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class ResearchLinks
    {
        public int id { get; set; }
        public int ric_form_1_Id { get; set; }  // Foreign key to RicForm1

        public string? linkage_type { get; set; }
        public DateTime? MoU_agreement_date { get; set; }
        public string? national_or_international { get; set; } // National or International
        public string? host_institution_name { get; set; }
        public string? host_institution_address { get; set; }
        public string? collaborating_agency_name { get; set; }
        public string? collaborating_agency_address { get; set; }
        public string? field_of_study { get; set; } // Broader research areas
        public string? scope_of_collaboration { get; set; }
        public string? salient_features { get; set; }
        public byte[]? evidence { get; set; } // Attachment

        [ForeignKey("ric_form_1_id")]
        public required ric_form_1 ric_form_1 { get; set; }
    }

}
