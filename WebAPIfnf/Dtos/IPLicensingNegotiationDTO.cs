namespace WebApi.Dtos
{
    public class IPLicensingNegotiationDTO
    {
        public int id { get; set; }
        public string? licensingtype { get; set; }
        public string? leadinventorname { get; set; }
        public string? leadinventordesignation { get; set; }
        public string? leadinventordepartment { get; set; }
        public string? inventiontitle { get; set; }
        public string? ipcategory { get; set; }
        public string? developmentstatus { get; set; }
        public string? keyscientificaspects { get; set; }
        public string? fieldofuse { get; set; }
        public string? durationofagreement { get; set; }
        public string? licensedetails { get; set; }
        public string? statusofnegotiation { get; set; }
        public string? evidence { get; set; }
    }
}
