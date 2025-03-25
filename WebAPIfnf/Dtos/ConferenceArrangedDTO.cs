namespace WebApi.Dtos
{
    public class ConferenceArrangedDTO
    {
        public int id { get; set; }
        public string? eventtype { get; set; }
        public string? eventlevel { get; set; }
        public string? title { get; set; }
        public DateTime eventdate { get; set; }
        public int? numberofparticipants { get; set; }
        public string? focusandoutcomes { get; set; }
        public string? organizer { get; set; }
        public string? audiencetype { get; set; }
        public string? evidence { get; set; }
    }
}
