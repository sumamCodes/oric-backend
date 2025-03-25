namespace WebApi.Models
{
    public class ric_form_2
    {
        public int ric_form_2_id { get; set; } // Primary Key
        public int dataoric_id { get; set; } // Foreign Key
        public required string faculty_name { get; set; }
        public required string department_name { get; set; }
        public required string faculty_email { get; set; }
        public int ip_disclosures_made { get; set; }
        public int patents_filed { get; set; }
        public int patents_granted { get; set; }
        public int ip_licensing_negotiations_initiated { get; set; }
        public int licenses_signed { get; set; }
        public int products_prototypes_developed { get; set; }
        public int products_prototypes_displayed { get; set; }
        public int industry_visits { get; set; }
        public int agreements_signed { get; set; }
        public int honors_awards_won { get; set; }
        public int oric_trainings_arranged { get; set; }
        public int external_trainings_arranged { get; set; }
        public int research_publications { get; set; }

        // Navigation Properties
        public ICollection<AgreementSigned> AgreementSigneds { get; set; } = new List<AgreementSigned>();
        public ICollection<ConferenceArranged> ConferenceArrangeds { get; set; } = new List<ConferenceArranged>();
        public ICollection<ExclusiveNonExclusive> ExclusiveorNonExclusives { get; set; } = new List<ExclusiveNonExclusive>();
        public ICollection<ExhibitionEvent> ExhibitionEvents { get; set; } = new List<ExhibitionEvent>();
        public ICollection<HonorOrAwards> HonorOrAwards { get; set; } = new List<HonorOrAwards>();
        public ICollection<IPDisclosure> IPDisclosures { get; set; } = new List<IPDisclosure>();
        public ICollection<IPLicensingNegotiation> IPLicensingNegotiations { get; set; } = new List<IPLicensingNegotiation>();
        public ICollection<Patent> Patents { get; set; } = new List<Patent>();
        public ICollection<ResearchProduct> ResearchProducts { get; set; } = new List<ResearchProduct>();
        public ICollection<ResearchPublication> ResearchPublications { get; set; } = new List<ResearchPublication>();
        public ICollection<ScienceArtsProduct> ScienceArtsProducts { get; set; } = new List<ScienceArtsProduct>();
        public ICollection<TrainingWorkshopSeminar> TrainingWorkshopSeminars { get; set; } = new List<TrainingWorkshopSeminar>();
        public ICollection<VisitRepresentative> VisitRepresentatives { get; set; } = new List<VisitRepresentative>();
    }
}
