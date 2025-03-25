using WebApi.Models;

public class ric_form_1
{
    public int ric_form_1_id { get; set; } // Matches the primary key in the database
    public int dataoric_id { get; set; } // Foreign key reference to dataoric
    public required string faculty_name { get; set; }
    public required string department_name { get; set; }
    public required string faculty_email { get; set; }
    public int research_grants_submitted_hec { get; set; }
    public int research_grants_submitted_non_hec { get; set; }
    public int research_grants_approved_hec { get; set; }
    public int research_grants_approved_non_hec { get; set; }
    public int hec_funded_projects_completed { get; set; }
    public int non_hec_funded_projects_completed { get; set; }
    public int joint_projects_submitted { get; set; }
    public int joint_projects_approved { get; set; }
    public int joint_projects_completed { get; set; }
    public int contract_research_awarded { get; set; }
    public int policy_advocacy_case_studies { get; set; }
    public int research_links_established { get; set; }
    public int civic_engagements { get; set; }
    public int consultancy_contracts_executed { get; set; }
    public int liaison_with_asrb { get; set; }

    // Navigation properties for subforms
    public ICollection<ResearchProjectSubmittedHEC>? ResearchProjectSubmittedHEC { get; set; }
    public ICollection<ResearchProjectSubmittedNonHEC>? ResearchProjectSubmittedNonHEC { get; set; }
    public ICollection<ResearchProjectApprovedHEC>? ResearchProjectApprovedHEC { get; set; }
    public ICollection<ResearchProjectApprovedNonHEC>? ResearchProjectApprovedNonHEC { get; set; }
    public ICollection<HECFundedResearchProjectCompleted>? HECFundedResearchProjectCompleted { get; set; }
    public ICollection<NonHECFundedResearchProjectCompleted>? NonHECFundedResearchProjectCompleted { get; set; }
    public ICollection<JointResearchProjectsSubmitted>? JointResearchProjectsSubmitted { get; set; }
    public ICollection<JointResearchProjectsApproved>? JointResearchProjectsApproved { get; set; }
    public ICollection<JointResearchProjectsCompleted>? JointResearchProjectsCompleted { get; set; }
    public ICollection<ContractResearchAwarded>? ContractResearchAwarded { get; set; }
    public ICollection<PolicyAdvocacies>? PolicyAdvocacies { get; set; }
    public ICollection<ResearchLinks>? ResearchLinks { get; set; }
    public ICollection<CivicEngagementEvents>? CivicEngagements { get; set; }
    public ICollection<ConsultancyContracts>? ConsultancyContracts { get; set; }
    public ICollection<LiaisonsASRB>? LiaisonsASRB { get; set; }

}
    