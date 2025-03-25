using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class ScienceArtsProduct
    {
        [ForeignKey("ric_form_2_id")]

        public required ric_form_2 ric_form_2 { get; set; }
        [Key]
        public int id { get; set; }

        public string? displayregion { get; set; } // "National" or "International"

        [Required]
        public string? title { get; set; }

        [Required]
        public string? leadname { get; set; }

        [Required]
        public string? leaddesignation { get; set; }

        [Required]
        public string? leaddepartment { get; set; }

        [Required]
        public string? productcategory { get; set; } // "Science", "Arts", "Design Product", etc.

        [Required]
        public string? forum { get; set; } // Forum where registered/performed/displayed

        [Required]
        public string? status { get; set; }

        public string? financialsupport { get; set; } // Optional

        [Required]
        public string? fieldofuse { get; set; }

        public byte[]? evidence { get; set; } // Path for uploaded file

    }
}
