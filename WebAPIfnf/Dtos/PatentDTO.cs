namespace WebApi.Dtos
{
    public class PatentDTO
    {
        public int id { get; set; }
        public string? filedorgranted { get; set; }
        public string? nationalorinternational { get; set; }
        public string? leadinventorname { get; set; }
        public string? leadinventordesignation { get; set; }
        public string? leadinventordepartment { get; set; }
        public string? inventiontitle { get; set; }
        public string? ipcategory { get; set; }
        public string? developmentstatus { get; set; }
        public string? keyscientificaspects { get; set; }
        public string? commercialpartner { get; set; }
        public string? patentfiledwith { get; set; }
        public string? patentgrantingauthority { get; set; }
        public string? financialsupport { get; set; }
        public DateTime? dateoffiling { get; set; }
        public string? filingevidence { get; set; }
        public string? grantingevidence { get; set; }
    }
}
