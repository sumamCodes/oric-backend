using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class VisitRepresentative
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("ric_form_2_id")]

        public required ric_form_2 ric_form_2 { get; set; }
        public string? visitorname { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime visitdate { get; set; }

        [Required]
        public string? agenda { get; set; }

        public byte[]? evidence { get; set; } // Path for uploaded file

    }
}
