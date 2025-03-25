using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class ExclusiveNonExclusive
    {
        [Key]
        public int id { get; set; }


        [ForeignKey("ric_form_2_id ")]

        public required ric_form_2 ric_form_2 { get; set; }
        public string? licensetype { get; set; } // "Exclusive" or "Non-Exclusive"

        [Required]
        public string? licenseregion { get; set; } // "National" or "International"

        [Required]
        public string? leadinventorname { get; set; }

        [Required]
        public string? leadinventordesignation { get; set; }

        [Required]
        public string? leadinventordepartment { get; set; }

        [Required]
        public string? inventiontitle { get; set; }

        [Required]
        public string? ipcategory { get; set; } // "Patent", "Trademark", etc.

        [Required]
        public string? developmentstatus { get; set; } // "Prototype", "Validation", etc.

        [Required]
        public string? keyscientificaspects { get; set; } // Milestones

        [Required]
        public string? fieldofuse { get; set; }

        [Required]
        public string? dateanddurationofagreement { get; set; }

        [Required]
        public string? licensedetails { get; set; } // Name, Organization

        public byte[]? evidence { get; set; }
    }
}
