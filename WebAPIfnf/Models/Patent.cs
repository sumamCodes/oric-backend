using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Patent
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ric_form_2_id ")]

        public required ric_form_2 ric_form_2 { get; set; }
        public string? filedorgranted { get; set; } // "Filed" or "Granted"

        [Required]
        public string? nationalorinternational { get; set; } // "National" or "International"

        [Required]
        public string? leadinventorname { get; set; }

        [Required]
        public string? leadinventordesignation { get; set; }

        [Required]
        public string? leadinventordepartment { get; set; }

        [Required]
        public string? inventiontitle { get; set; }

        [Required]
        public string? IPcategory { get; set; } // "Patent", "Trade marks", "Design patent", etc.

        [Required]
        public string? developmentstatus { get; set; } // "Idea", "Prototype", etc.

        [Required]
        public string? keyscientificaspects { get; set; }

        public string? commercialpartner { get; set; }

        [Required]
        public string? patentfiledwith { get; set; } // Name and details of the patent department

        [Required]
        public string? patentgrantingauthority { get; set; } // Name and details of the granting authority

        public string? financialsupport { get; set; }

        [Required]
        public DateTime dateoffiling { get; set; }
        public byte[]? filingevidence { get; set; }
        public byte[]? grantingevidence { get; set; }

    }
}
