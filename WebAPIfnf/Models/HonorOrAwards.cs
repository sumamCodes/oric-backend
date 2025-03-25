using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class HonorOrAwards
    {
        [Key] public int id{ get; set; }

        [ForeignKey("ric_form_2_id ")]

        public required ric_form_2 ric_form_2 { get; set; }

        [Required] public string? Title { get; set; }
        [Required] public string? forumororganization { get; set; }
        [Required] public string? awardreceived { get; set; }
        [Required] public string? workdetails { get; set; }

        public decimal? prizemoney { get; set; }

        [Required] public string? awardwinnerdetails { get; set; }

        public string? remarks { get; set; }
        public byte[]? evidence { get; set; }
    }
}
