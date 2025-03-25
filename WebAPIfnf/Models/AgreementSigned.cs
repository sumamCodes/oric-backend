using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class AgreementSigned
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("ric_form_2_id")]
        public required ric_form_2 ric_form_2 { get; set; }
        public string? typeoflinkage { get; set; } // Academic, Research

        [Required]
        public string? nationalorinternational { get; set; } // National, International

        [Required]
        public string? hostinstitutionnameandaddress { get; set; }

        [Required]
        public string? duration { get; set; }

        [Required]
        public string? keyinitiatives { get; set; }

        [Required]
        public string? field { get; set; }

        [Required]
        public string? scopeofcollaboration { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime linkageestablishmentdate { get; set; }

        public string? financialsupport { get; set; }
        public byte[]? evidence { get; set; }
    }
}
