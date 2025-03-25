namespace WebApi.Dtos
{
    public class ResearchProjectSubmittedHECDTO
    {
        public int id { get; set; }
        public int ric_form_1_id { get; set; }  

        public string? research_grant_name { get; set; } 
        public DateTime? proposal_submission_date { get; set; }
        public string? PI_name { get; set; } 
        public string? designation { get; set; }
        public string? department { get; set; }
        public string? thematic_area { get; set; }
        public string? research_proposal_title { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public decimal? total_funding_requested { get; set; } 
        public string? collaborating_partners { get; set; } 
        public string? co_funding_partners { get; set; } 
        public string? status { get; set; }
        public string? remarks { get; set; } 
        public string? evidence { get; set; }    
    }
}
