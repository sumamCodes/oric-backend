using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class ResearchProduct
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("ric_form_2_id ")]

        public required ric_form_2 ric_form_2 { get; set; }
        public string? researchregion { get; set; } // "National" or "International"

        [Required]
        public string? leadinventorname { get; set; }

        [Required]
        public string? leadinventordesignation { get; set; }

        [Required]
        public string? leadinventordepartment { get; set; }

        [Required]
        public string? inventiontitle { get; set; }

        [Required]
        public string? IPcategory { get; set; } // "Patent", "Trade mark", etc.

        [Required]
        public string? developmentstatus { get; set; } // "Prototype", "Validation", etc.

        [Required]
        public string? keyscientificaspects { get; set; } // Milestones

        [Required]
        public string? fieldofuse { get; set; }

        [Required]
        public string? collaboratingindustrialpartner { get; set; }

        public string? financialsupport { get; set; } // Optional

        public string? remarks { get; set; } // Optional remarks

        public byte[]? evidence { get; set; }
    }
}
