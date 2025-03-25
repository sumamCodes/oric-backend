using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Events
    {
        [Key]
        public int event_id { get; set; }

        [Required]
        public int ric_form_3_id { get; set; }

        public string? title { get; set; }
        public DateTime? event_date { get; set; }
        public string? venue { get; set; }
        public string? field { get; set; }
        public string? panelist_details { get; set; }
        public string? organizers { get; set; }
        public string? audience { get; set; }
        public int? participants_count { get; set; }
        public byte[]? evidence { get; set; }

        [ForeignKey("ric_form_3_id")]
        public required RicForm3 RicForm3 { get; set; }
    }
}
