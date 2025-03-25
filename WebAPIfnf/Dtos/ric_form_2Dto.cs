namespace WebApi.Dtos
{
    public class ric_form_2Dto
    {
        public int ric_form_2_id { get; set; } // Primary key
        public int dataoric_id { get; set; } // Foreign key reference to dataoric
        public required string faculty_name { get; set; }
        public required string department_name { get; set; }
        public required string faculty_email { get; set; }
        public int ip_disclosures_made { get; set; }
        public int patents_filed { get; set; }
        public int patents_granted { get; set; }
        public int ip_licensing_negotiations_initiated { get; set; }
        public int licenses_signed { get; set; }
        public int products_prototypes_developed { get; set; }
        public int products_prototypes_displayed { get; set; }
        public int industry_visits { get; set; }
        public int agreements_signed { get; set; }
        public int honors_awards_won { get; set; }
        public int oric_trainings_arranged { get; set; }
        public int external_trainings_arranged { get; set; }
        public int research_publications { get; set; }
    }
}
