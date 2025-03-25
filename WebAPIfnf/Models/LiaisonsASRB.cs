using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class LiaisonsASRB
    {
        public int id { get; set; }
        public int ric_form_1_id { get; set; }  // Foreign key to RicForm1

        public string? liaison_with { get; set; }
        public DateTime? execution_date { get; set; }
        public byte[]? evidence { get; set; } // Attachment

        // Navigation property
        [ForeignKey("ric_form_1_id")]
        public required ric_form_1 ric_form_1 { get; set; }
    }

}
