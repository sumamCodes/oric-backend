using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos
{
    public class SpinOffsDto
    {
        public int spinoff_id { get; set; }
        public int ric_form_3_id { get; set; }
        public string? spinoff_name { get; set; }
        public string? stage { get; set; }
        public string? license_agreement { get; set; }
        public decimal? revenue { get; set; }
        public string? evidence { get; set; }
    }
}
