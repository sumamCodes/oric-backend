namespace WebApi.Dtos
{
    public class VisitRepresentativeDTO
    {
        public int id { get; set; }
        public string? visitorname { get; set; }
        public DateTime visitdate { get; set; }
        public string? agenda { get; set; }
        public string? evidence { get; set; }
    }
}
