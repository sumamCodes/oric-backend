using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Funding
    {
        [Key]
        public int funding_id { get; set; }

        [Required]
        public int ric_form_3_id { get; set; }

        public string? startup_details { get; set; }
        public string? funding_agency { get; set; }
        public string? funding_type { get; set; }
        public decimal? amount { get; set; }
        public string? agreement_signed { get; set; }
        public string? in_kind_support { get; set; }
        public byte[]? evidence { get; set; }

        [ForeignKey("ric_form_3_id")]
        public required RicForm3 RicForm3 { get; set; }
    }
}
