using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos
{
    public class EventsDto
    {
        public int event_id { get; set; }
        public int ric_form_3_id { get; set; }
        public string? title { get; set; }
        public DateTime? event_date { get; set; }
        public string? venue { get; set; }
        public string? field { get; set; }
        public string? panelist_details { get; set; }
        public string? organizers { get; set; }
        public string? audience { get; set; }
        public int? participants_count { get; set; }
        public string? evidence { get; set; }
    }
}
