﻿namespace WebApi.Dtos
{
    public class JointResearchProjectsSubmittedDTO
    {
        public int id { get; set; }
        public int ric_form_1_id { get; set; }  // Foreign key to RicForm1

        public string? joint_research_grant_name { get; set; }
        public string? funding_agency { get; set; }
        public DateTime? submission_date { get; set; }
        public string? national_or_international { get; set; }
        public string? PI_name { get; set; }
        public string? designation { get; set; }
        public string? department { get; set; }
        public string? co_PI_name { get; set; }
        public string? co_PI_designation { get; set; }
        public string? co_PI_department { get; set; }
        public string? co_PI_University { get; set; }
        public string? thematic_area { get; set; }
        public string? research_proposal_title { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public decimal? total_funding_requested { get; set; } // (PKR Million)
        public string? co_funding_partners_details { get; set; }
        public string? status { get; set; }
        public string? remarks { get; set; }
        public string? evidence { get; set; } // Attachment
    }
}
