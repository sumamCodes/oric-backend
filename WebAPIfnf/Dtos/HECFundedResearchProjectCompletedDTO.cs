namespace WebApi.Dtos
{
    public class HECFundedResearchProjectCompletedDTO
    {
        public int id { get; set; }
        public int ric_form_1_id { get; set; }  // Foreign key to RicForm1

        public string? research_grant_name { get; set; }
        public DateTime? project_completion_date { get; set; }
        public string? PI_name { get; set; }
        public string? designation { get; set; }
        public string? department { get; set; }
        public string? thematic_area { get; set; }
        public string? research_proposal_title { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public string? project_status { get; set; } // Options: Completed, In Progress, Delayed
        public decimal? total_funding_approved { get; set; } // (PKR Million)
        public decimal? total_funding_released { get; set; } // (PKR Million)
        public string? key_project_deliverables { get; set; } // Attachment
        public string? evidence { get; set; }
    }
}
