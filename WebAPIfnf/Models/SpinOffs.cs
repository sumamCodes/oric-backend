using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class SpinOffs
    {
        [Key]
        public int spinoff_id { get; set; }

        [Required]
        public int ric_form_3_id { get; set; }

        public string? spinoff_name { get; set; }
        public string? stage { get; set; }
        public string? license_agreement { get; set; }
        public decimal? revenue { get; set; }
        public byte[]? evidence { get; set; }

        [ForeignKey("ric_form_3_id")]
        public required RicForm3 RicForm3 { get; set; }
    }
}
