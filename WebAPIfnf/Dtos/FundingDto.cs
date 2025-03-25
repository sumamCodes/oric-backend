using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos
{
    public class FundingDto
    {
        public int funding_id { get; set; }

        public int ric_form_3_id { get; set; }
        public string? startup_details { get; set; }
        public string? funding_agency { get; set; }
        public string? funding_type { get; set; }
        public decimal? amount { get; set; }
        public string? agreement_signed { get; set; }
        public string? in_kind_support { get; set; }
        public string? evidence { get; set; }
    }
}
