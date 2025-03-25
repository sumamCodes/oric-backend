namespace WebApi.Dtos
{
    public class ric_form_1Dto
    {
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

        // Nullable Subforms (each can have multiple entries)
        public List<ResearchProjectSubmittedHECDTO>? ResearchProjectsSubmittedHEC { get; set; }
        public List<ResearchProjectSubmittedNonHECDTO>? ResearchProjectsSubmittedNonHEC { get; set; }
        public List<ResearchProjectApprovedHECDTO>? ResearchProjectsApprovedHEC { get; set; }
        public List<ResearchProjectApprovedNonHECDTO>? ResearchProjectsApprovedNonHEC { get; set; }
        public List<HECFundedResearchProjectCompletedDTO>? HECFundedResearchCompleted { get; set; }
        public List<NonHECFundedResearchProjectCompletedDTO>? NonHECFundedResearchCompleted { get; set; }
        public List<JointResearchProjectsSubmittedDTO>? JointResearchSubmitted { get; set; }
        public List<JointResearchProjectsApprovedDTO>? JointResearchApproved { get; set; }
        public List<JointResearchProjectsCompletedDTO>? JointResearchCompleted { get; set; }
        public List<ContractResearchAwardedDTO>? ContractResearchAwarded { get; set; }
        public List<PolicyAdvocacyDTO>? PolicyAdvocacy { get; set; }
        public List<ResearchLinkageDTO>? ResearchLinks { get; set; }
        public List<CivicEngagementDTO>? CivicEngagement { get; set; }
        public List<ConsultancyContractDTO>? ConsultancyContracts { get; set; }
        public List<LiaisonDevelopedDTO>? LiaisonDevelopedWithASRB { get; set; }
    }
}
//        // New properties
//    // Research Projects Submitted
//    public string? research_project_submitted_type { get; set; }
//    public string? submitted_grant_name { get; set; }
//    public DateOnly? submitted_proposal_submission_date { get; set; }
//    public string? submitted_pi_name { get; set; }
//    public string? submitted_pi_designation { get; set; }
//    public string? submitted_pi_department { get; set; }
//    public string? submitted_thematic_area { get; set; }
//    public string? submitted_research_title { get; set; }
//    public DateOnly? submitted_duration_start { get; set; }
//    public DateOnly? submitted_duration_end { get; set; }
//    public decimal? submitted_total_funding_requested { get; set; }
//    public string? submitted_collaborating_partners { get; set; }
//    public string? submitted_co_funding_partners { get; set; }
//    public string? submitted_national_or_international { get; set; }
//    public string? submitted_status { get; set; }
//    public string? submitted_remarks { get; set; }
//    public byte[]? submitted_evidence { get; set; }

//    // Research Projects Approved
//    public string? approved_grant_name { get; set; }
//    public DateOnly? approved_proposal_approval_date { get; set; }
//    public string? approved_pi_name { get; set; }
//    public string? approved_pi_designation { get; set; }
//    public string? approved_pi_department { get; set; }
//    public string? approved_thematic_area { get; set; }
//    public string? approved_research_title { get; set; }
//    public DateOnly? approved_duration_start { get; set; }
//    public DateOnly? approved_duration_end { get; set; }
//    public decimal? approved_total_funding_approved { get; set; }
//    public string? approved_collaborating_partners { get; set; }
//    public string? approved_co_funding_partners { get; set; }
//    public string? approved_national_or_international { get; set; }
//    public string? approved_status { get; set; }
//    public string? approved_remarks { get; set; }
//    public byte[]? approved_evidence { get; set; }

//    // Research Projects Completed
//    public string? completed_grant_name { get; set; }
//    public DateOnly? completed_project_completion_date { get; set; }
//    public string? completed_pi_name { get; set; }
//    public string? completed_pi_designation { get; set; }
//    public string? completed_pi_department { get; set; }
//    public string? completed_thematic_area { get; set; }
//    public string? completed_research_title { get; set; }
//    public DateOnly? completed_duration_start { get; set; }
//    public DateOnly? completed_duration_end { get; set; }
//    public decimal? completed_total_funding_utilized { get; set; }
//    public decimal? completed_total_funding_released { get; set; }
//    public string? completed_status { get; set; }
//    public string? completed_key_deliverables { get; set; }
//    public byte[]? completed_evidence { get; set; }

//    // Joint Research Projects
//    public string? joint_research_type { get; set; }
//    public string? joint_grant_name { get; set; }
//    public string? joint_funding_agency { get; set; }
//    public DateOnly? joint_project_submission_date { get; set; }
//    public DateOnly? joint_project_approval_date { get; set; }
//    public DateOnly? joint_project_completion_date { get; set; }
//    public string? joint_pi_name { get; set; }
//    public string? joint_pi_designation { get; set; }
//    public string? joint_pi_department { get; set; }
//    public string? joint_co_pi_name { get; set; }
//    public string? joint_co_pi_designation { get; set; }
//    public string? joint_co_pi_department { get; set; }
//    public string? joint_co_pi_university { get; set; }
//    public string? joint_thematic_area { get; set; }
//    public string? joint_research_title { get; set; }
//    public DateOnly? joint_duration_start { get; set; }
//    public DateOnly? joint_duration_end { get; set; }
//    public decimal? joint_total_funding { get; set; }
//    public string? joint_co_funding_partners { get; set; }
//    public string? joint_status { get; set; }
//    public string? joint_remarks { get; set; }
//    public byte[]? joint_evidence { get; set; }

//    // Contract Research Awarded
//    public string? contract_thematic_area { get; set; }
//    public string? contract_research_title { get; set; }
//    public DateOnly? contract_signed_date { get; set; }
//    public string? contract_pi_name { get; set; }
//    public string? contract_pi_designation { get; set; }
//    public string? contract_pi_department { get; set; }
//    public string? contract_co_pi_name { get; set; }
//    public string? contract_co_pi_designation { get; set; }
//    public string? contract_co_pi_department { get; set; }
//    public string? contract_sponsoring_agency { get; set; }
//    public string? contract_sponsoring_address { get; set; }
//    public string? contract_national_or_international { get; set; }
//    public string? contract_counterpart_industry { get; set; }
//    public DateOnly? contract_duration_start { get; set; }
//    public DateOnly? contract_duration_end { get; set; }
//    public decimal? contract_total_amount { get; set; }
//    public string? contract_expected_deliverables { get; set; }
//    public DateOnly? contract_date { get; set; }
//    public byte[]? contract_evidence { get; set; }

//    // Policy Advocacy
//    public string? policy_advocacy_body { get; set; }
//    public DateOnly? policy_advocacy_date { get; set; }
//    public string? policy_advocacy_pi_name { get; set; }
//    public string? policy_advocacy_area { get; set; }
//    public string? policy_advocacy_brief { get; set; }
//    public DateOnly? policy_advocacy_duration_start { get; set; }
//    public DateOnly? policy_advocacy_duration_end { get; set; }
//    public string? policy_advocacy_coalition_partners { get; set; }
//    public string? policy_advocacy_research_status { get; set; }
//    public string? policy_advocacy_tools { get; set; }
//    public byte[]? policy_advocacy_evidence { get; set; }

//    // Research Links
//    public string? research_links_type { get; set; }
//    public DateOnly? research_links_mou_date { get; set; }
//    public string? research_links_national_or_international { get; set; }
//    public string? research_links_host_institution { get; set; }
//    public string? research_links_collaborating_agency { get; set; }
//    public string? research_links_field_of_study { get; set; }
//    public string? research_links_scope { get; set; }
//    public string? research_links_salient_features { get; set; }
//    public byte[]? research_links_evidence { get; set; }

//    // Civic Engagement Events
//    public string? civic_event_title { get; set; }
//    public DateOnly? civic_event_date { get; set; }
//    public string? civic_event_component { get; set; }
//    public string? civic_event_outcome { get; set; }
//    public string? civic_event_collaboration { get; set; }
//    public string? civic_event_csos { get; set; }
//    public string? civic_event_sponsoring_agency { get; set; }
//    public decimal? civic_event_grant_value { get; set; }
//    public string? civic_event_participation { get; set; }
//    public string? civic_event_dissemination_material { get; set; }
//    public string? civic_event_remarks { get; set; }
//    public byte[]? civic_event_evidence { get; set; }

//    // Consultancy Contracts
//    public string? consultancy_title { get; set; }
//    public DateOnly? consultancy_execution_date { get; set; }
//    public string? consultancy_pi_name { get; set; }
//    public string? consultancy_pi_designation { get; set; }
//    public string? consultancy_pi_department { get; set; }
//    public string? consultancy_company_details { get; set; }
//    public decimal? consultancy_contract_value { get; set; }
//    public DateOnly? consultancy_project_timelines_start { get; set; }
//    public DateOnly? consultancy_project_timelines_end { get; set; }
//    public string? consultancy_type_of_services { get; set; }
//    public string? consultancy_key_deliverables { get; set; }
//    public decimal? consultancy_oric_percentage { get; set; }
//    public string? consultancy_remarks { get; set; }
//    public byte[]? consultancy_evidence { get; set; }

//    // Liaison with AS&RB
//    public string? liaison_developed_with { get; set; }
//    public DateOnly? liaison_execution_date { get; set; }
//    public byte[]? liaison_evidence { get; set; }
//    }
//}
