namespace WebApi.Dtos
{
    public class LiaisonDevelopedDTO
    {
        public int id { get; set; }
        public int ric_form_1_id { get; set; }  // Foreign key to RicForm1

        public string? liaison_with { get; set; }
        public DateTime? execution_date { get; set; }
        public string? evidence { get; set; } // Attachment

    }
}
