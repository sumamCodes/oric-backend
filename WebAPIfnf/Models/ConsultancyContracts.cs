using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class ConsultancyContracts
    {
        public int id { get; set; }
        public int ric_form_1_id { get; set; }  // Foreign key to RicForm1

        public string? project_title { get; set; }
        public DateTime? execution_date { get; set; }
        public string? PI_name { get; set; }
        public string? PI_designation { get; set; }
        public string? PI_department { get; set; }
        public string? company_details { get; set; } // Name, Country, etc.
        public decimal? contract_value { get; set; } // PKR Millions
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public string? consultancy_type { get; set; } // Feasibility, Prototype Development, Testing, Analysis, etc.
        public string? key_deliverables { get; set; }
        public decimal? ORIC_percentage { get; set; } // Optional
        public string? remarks { get; set; }
        public byte[]? evidence { get; set; } // Attachment

        [ForeignKey("ric_form_1_id")]
        public required ric_form_1 ric_form_1 { get; set; }
    }

}
