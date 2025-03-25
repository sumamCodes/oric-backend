using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class JointResearchProjectsCompleted
    {
        public int id { get; set; }
        public int ric_form_1_id { get; set; }  // Foreign key to RicForm1

        public string? joint_research_grant_name { get; set; }
        public string? funding_agency { get; set; }
        public DateTime? completion_date { get; set; }
        public string? national_or_international { get; set; }
        public string? PI_name { get; set; }
        public string? designation { get; set; }
        public string? department { get; set; }
        public string? co_PI_name { get; set; }
        public string? co_PI_designation { get; set; }
        public string? co_PI_department { get; set; }
        public string? co_PI_university { get; set; }
        public string? thematic_area { get; set; }
        public string? research_proposal_title { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public decimal? total_funding_utilized { get; set; } // (PKR Million)
        public decimal? total_funding_requested { get; set; } // (PKR)
        public string? co_funding_partners_details { get; set; }
        public string? status { get; set; } // Options: Completed, In Process, Delayed
        public byte[]? key_project_deliverables { get; set; } // Attachment (Brief summary)
        public byte[]? evidence { get; set; } // Attachment

        // Navigation property
        [ForeignKey("ric_form_1_id")]
        public required ric_form_1 ric_form_1 { get; set; }

    }
}
