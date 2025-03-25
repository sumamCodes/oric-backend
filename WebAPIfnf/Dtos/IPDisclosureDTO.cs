namespace WebApi.Dtos
{
    public class IPDisclosureDTO
    {
        public int id { get; set; }
        public string? leadinventorname { get; set; }
        public string? leadinventordesignation { get; set; }
        public string? leadinventordepartment { get; set; }
        public string? inventiontitle { get; set; }
        public string? ipcategory { get; set; }
        public string? developmentstatus { get; set; }
        public string? keyscientificaspects { get; set; }
        public string? commercialpartner { get; set; }
        public string? disclosuremadewith { get; set; }
        public DateTime? disclosuremadedate { get; set; }
        public string? financialsupport { get; set; }
        public string? previousdisclosure { get; set; }
        public string? evidence { get; set; }
    }
}
