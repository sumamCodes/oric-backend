using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos
{
    public class FacultyStartupsDto
    {
        public int startup_id { get; set; }
        public int ric_form_3_id { get; set; }
        public string? startup_name { get; set; }
        public string? sector { get; set; }
        public string? stage { get; set; }
        public string? ip_status { get; set; }
        public string? license_agreement { get; set; }
        public string? funding_source { get; set; }
        public decimal? revenue { get; set; }
        public int? internships_created { get; set; }
        public int? jobs_created { get; set; }
        public string? evidence { get; set; }
    }
}
