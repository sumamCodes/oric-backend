namespace WebApi.Dtos
{
    public class RicFormFilterDto
    {
        public string? faculty_name { get; set; }
        public string? department_name { get; set; }
        public string? faculty_email { get; set; }
        public int? research_grants_submitted_hec { get; set; }
        public int? research_grants_approved_non_hec { get; set; }
        // Add other properties if you want to support filtering by them
    }
}
