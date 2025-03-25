using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.SqlServer.Server;

namespace WebApi.Models
{
    public class IPLicensingNegotiation
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("ric_form_2_id ")]

        public required ric_form_2 ric_form_2 { get; set; }
        public string? licensingtype { get; set; } // "National" or "International"

        [Required]
        public string? leadinventorname { get; set; }

        [Required]
        public string? leadinventordesignation { get; set; }

        [Required]
        public string? leadinventordepartment { get; set; }

        [Required]
        public string? inventiontitle { get; set; }

        [Required]
        public string? IPcategory { get; set; } // "Patents", "Trade marks", etc.

        [Required]
        public string? developmentstatus { get; set; } // "Prototype", "Validation", etc.

        [Required]
        public string? keyscientificaspects { get; set; }

        [Required]
        public string? fieldofuse { get; set; }

        [Required]
        public string? durationofagreement { get; set; }

        [Required]
        public string? licensedetails { get; set; } // Name, Organization

        [Required]
        public string? statusofnegotiation { get; set; }

        public byte[]? evidence { get; set; }
    }
}
