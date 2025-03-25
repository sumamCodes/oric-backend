namespace WebApi.Dtos
{
    public class ResearchProjectApprovedHECDTO
    {
        public int id { get; set; }
        public int ric_form_1_id { get; set; }  // Foreign key to RicForm1

        public string? research_grant_name { get; set; }
        public DateTime? proposal_approval_date { get; set; }
        public string? national_or_international { get; set; }
        public string? PI_name { get; set; } // Principal Investigator
        public string? designation { get; set; }
        public string? department { get; set; }
        public string? thematic_area { get; set; }
        public string? research_proposal_title { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public decimal? total_funding_approved { get; set; } // (PKR Million)
        public string? collaborating_partners { get; set; } // Nullable
        public string? co_funding_partners { get; set; } // Nullable
        public DateTime? approval_date { get; set; }
        public string? evidence { get; set; }
    }
}
