using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class ContractResearchAwarded
    {
        public int id { get; set; }
        public int ric_form_1_id { get; set; }  // Foreign key to RicForm1

        public string? thematic_area { get; set; }
        public string? research_proposal_title { get; set; }
        public DateTime? contract_signed_date { get; set; }
        public string? PI_name { get; set; }
        public string? PI_designation { get; set; }
        public string? PI_department { get; set; }
        public string? co_PI_name { get; set; }
        public string? co_PI_designation { get; set; }
        public string? co_PI_department { get; set; }
        public string? co_PI_university { get; set; }
        public string? sponsoring_agency_name { get; set; }
        public string? sponsoring_agency_address { get; set; }
        public string? sponsoring_agency_country { get; set; }
        public string?  national_or_international { get; set; } // Options: National, International
        public string? counterpart_industry { get; set; } // Address with country
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public decimal? total_amount_approved { get; set; } // (PKR Millions)
        public string? expected_deliverables { get; set; }
        public byte[]? evidence { get; set; } // Attachment

        // Navigation property
        [ForeignKey("ric_form_1_id")]
        public required ric_form_1 ric_form_1 { get; set; }
    }

}
