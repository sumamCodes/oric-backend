using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class ResearchPublication
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ric_form_2_id ")]
        public required ric_form_2 ric_form_2 { get; set; }


        [Required]
        public string? publicationcategory { get; set; } // W, X, Y (HJRS Categories)

        [Required]
        public string? publicationreference { get; set; } // Full reference of the publication

        [Required]
        [Url]
        public string? publicationlink { get; set; } // URL of the publication

        public byte[]? evidence { get; set; } // Path for uploaded first page of the publication

    }
}
