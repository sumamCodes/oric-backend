namespace WebApi.Dtos
{
    public class ResearchLinkageDTO
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
        public string? evidence { get; set; } // Attachment

    }
}
