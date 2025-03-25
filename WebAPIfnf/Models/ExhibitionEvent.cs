using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class ExhibitionEvent
    {
        [Key]
        public int id { get; set; }


        [ForeignKey("ric_form_2_id ")]

        public required ric_form_2 ric_form_2 { get; set; }
        public string? eventlevel { get; set; }

        [Required]
        public string? eventtype { get; set; }

        [Required]
        public DateTime eventdate { get; set; }

        [Required]
        public int? numberofparticipants { get; set; }

        [Required]
        public string? focusandoutcomes { get; set; }

        [Required]
        public string? audiencetype { get; set; }

        public byte[]? evidence { get; set; }
    }
}
