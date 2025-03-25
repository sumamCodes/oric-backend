namespace WebApi.Dtos
{
    public class PolicyAdvocacyDTO
    {
        public int id { get; set; }
        public int ric_form_1_id { get; set; }  // Foreign key to RicForm1

        public string? government_body_presented { get; set; }
        public DateTime? presentation_date { get; set; }
        public string? PI_name { get; set; }
        public string? PI_designation { get; set; }
        public string? PI_department { get; set; }
        public string? advocacy_area { get; set; } // Political, Law & Order, Economic Development, Social Progress, etc.
        public string? brief { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public string? coalition_partners { get; set; } // If any
        public string? research_status { get; set; } // Issue verification, backing research status
        public string? advocacy_tools { get; set; } // Briefings, meetings, social media, etc.
        public string? evidence { get; set; } // Attachment
    }
}
