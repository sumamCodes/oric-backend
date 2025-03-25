using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dtos;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RicForm1Controller : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RicForm1Controller> _logger;

        public RicForm1Controller(ApplicationDbContext context, ILogger<RicForm1Controller> logger)
        {
            _context = context;
            _logger = logger;
        }

        [Authorize]
        [HttpPost("submit")]
        public async Task<IActionResult> SubmitRicForm1(ric_form_1Dto dto)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("Invalid user ID in token.");
            }

            var ricForm = new ric_form_1
            {
                dataoric_id = userId,
                faculty_name = dto.faculty_name,
                department_name = dto.department_name,
                faculty_email = dto.faculty_email,
                research_grants_submitted_hec = dto.research_grants_submitted_hec,
                research_grants_submitted_non_hec = dto.research_grants_submitted_non_hec,
                research_grants_approved_hec = dto.research_grants_approved_hec,
                research_grants_approved_non_hec = dto.research_grants_approved_non_hec,
                hec_funded_projects_completed = dto.hec_funded_projects_completed,
                non_hec_funded_projects_completed = dto.non_hec_funded_projects_completed,
                joint_projects_submitted = dto.joint_projects_submitted,
                joint_projects_approved = dto.joint_projects_approved,
                joint_projects_completed = dto.joint_projects_completed,
                contract_research_awarded = dto.contract_research_awarded,
                policy_advocacy_case_studies = dto.policy_advocacy_case_studies,
                research_links_established = dto.research_links_established,
                civic_engagements = dto.civic_engagements,
                consultancy_contracts_executed = dto.consultancy_contracts_executed,
                liaison_with_asrb = dto.liaison_with_asrb
            };

            _context.ric_form_1.Add(ricForm);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "RIC Form 1 submitted successfully.", RicForm1Id = ricForm.ric_form_1_id });
        }

        [Authorize]
        [HttpPost("research_project_submitted_hec")]
        public async Task<IActionResult> SubmitResearchProjectSubmittedHEC(ResearchProjectSubmittedHECDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            var ricForm = await _context.ric_form_1.FindAsync(dto.ric_form_1_id);
            if (ricForm == null)
            {
                return NotFound("RicForm1 record not found.");
            }

            var researchProject = new ResearchProjectSubmittedHEC
            {
                ric_form_1 = ricForm,
                research_grant_name = dto.research_grant_name,
                proposal_submission_date = dto.proposal_submission_date,
                PI_name = dto.PI_name,
                designation = dto.designation,
                department = dto.department,
                thematic_area = dto.thematic_area,
                research_proposal_title = dto.research_proposal_title,
                start_date = dto.start_date,
                end_date = dto.end_date,
                total_funding_requested = dto.total_funding_requested,
                collaborating_partners = dto.collaborating_partners,
                co_funding_partners = dto.co_funding_partners,
                status = dto.status,
                remarks = dto.remarks,
                evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : null

            };
            _context.ResearchProjectSubmittedHEC.Add(researchProject);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Research project submitted successfully." });
        }

        [Authorize]
        [HttpPost("research_project_submitted_non_hec")]
        public async Task<IActionResult> SubmitResearchProjectSubmittedNonHEC(ResearchProjectSubmittedNonHECDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            var ricForm = await _context.ric_form_1.FindAsync(dto.ric_form_1_id);
            if (ricForm == null)
            {
                return NotFound("RicForm1 record not found.");
            }

            var researchProject = new ResearchProjectSubmittedNonHEC
            {
                ric_form_1_id = dto.ric_form_1_id,
                ric_form_1 = ricForm,
                research_grant_name = dto.research_grant_name,
                proposal_submission_date = dto.proposal_submission_date,
                national_or_international = dto.national_or_international,
                PI_name = dto.PI_name,
                designation = dto.designation,
                department = dto.department,
                thematic_area = dto.thematic_area,
                research_proposal_title = dto.research_proposal_title,
                start_date = dto.start_date,
                end_date = dto.end_date,
                total_funding_requested = dto.total_funding_requested,
                collaborating_partners = dto.collaborating_partners,
                co_funding_partners = dto.co_funding_partners,
                status = dto.status,
                remarks = dto.remarks,
                evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : null
            };

            _context.ResearchProjectSubmittedNonHEC.Add(researchProject);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Research Project Submitted (Non-HEC) saved successfully." });
        }

        [Authorize]
        [HttpPost("submit-research-project-approved-hec")]
        public async Task<IActionResult> SubmitResearchProjectApprovedHEC(ResearchProjectApprovedHECDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            var ricForm = await _context.ric_form_1.FindAsync(dto.ric_form_1_id);
            if (ricForm == null)
            {
                return NotFound("RicForm1 record not found.");
            }

            var researchProject = new ResearchProjectApprovedHEC
            {
                ric_form_1_id = dto.ric_form_1_id,
                ric_form_1 = ricForm,
                research_grant_name = dto.research_grant_name,
                proposal_approval_date = dto.proposal_approval_date,
                national_or_international = dto.national_or_international,
                PI_name = dto.PI_name,
                designation = dto.designation,
                department = dto.department,
                thematic_area = dto.thematic_area,
                research_proposal_title = dto.research_proposal_title,
                start_date = dto.start_date,
                end_date = dto.end_date,
                total_funding_approved = dto.total_funding_approved,
                collaborating_partners = dto.collaborating_partners,
                co_funding_partners = dto.co_funding_partners,
                approval_date = dto.approval_date,
                evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : null
            };

            _context.ResearchProjectApprovedHEC.Add(researchProject);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Research Project Approved (HEC) submitted successfully.", ResearchProjectApprovedHECId = researchProject.id });
        }

        [Authorize]
        [HttpPost("submit-research-project-approved-non-hec")]
        public async Task<IActionResult> SubmitResearchProjectApprovedNonHEC(ResearchProjectApprovedNonHECDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            var ricForm = await _context.ric_form_1.FindAsync(dto.ric_form_1_id);
            if (ricForm == null)
            {
                return NotFound("RicForm1 record not found.");
            }

            var researchProject = new ResearchProjectApprovedNonHEC
            {
                ric_form_1_id = dto.ric_form_1_id,
                ric_form_1 = ricForm,
                research_grant_name = dto.research_grant_name,
                proposal_approval_date = dto.proposal_approval_date,
                national_or_international = dto.national_or_international,
                PI_name = dto.PI_name,
                designation = dto.designation,
                department = dto.department,
                thematic_area = dto.thematic_area,
                research_proposal_title = dto.research_proposal_title,
                start_date = dto.start_date,
                end_date = dto.end_date,
                total_funding_approved = dto.total_funding_approved,
                collaborating_partners = dto.collaborating_partners,
                co_funding_partners = dto.co_funding_partners,
                approval_date = dto.approval_date,
                evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : null
            };

            _context.ResearchProjectApprovedNonHEC.Add(researchProject);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Research Project Approved (Non-HEC) submitted successfully.", ResearchProjectApprovedNonHECId = researchProject.id });
        }

        [Authorize]
        [HttpPost("submit-hec-funded-research-project-completed")]
        public async Task<IActionResult> SubmitHECFundedResearchProjectCompleted(HECFundedResearchProjectCompletedDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            var ricForm = await _context.ric_form_1.FindAsync(dto.ric_form_1_id);
            if (ricForm == null)
            {
                return NotFound("RicForm1 record not found.");
            }

            var projectCompleted = new HECFundedResearchProjectCompleted
            {
                ric_form_1_id = dto.ric_form_1_id,
                ric_form_1 = ricForm,
                research_grant_name = dto.research_grant_name,
                project_completion_date = dto.project_completion_date,
                PI_name = dto.PI_name,
                designation = dto.designation,
                department = dto.department,
                thematic_area = dto.thematic_area,
                research_proposal_title = dto.research_proposal_title,
                start_date = dto.start_date,
                end_date = dto.end_date,
                project_status = dto.project_status,
                total_funding_approved = dto.total_funding_approved,
                total_funding_released = dto.total_funding_released,
                key_project_deliverables = !string.IsNullOrEmpty(dto.key_project_deliverables) ? FileHelper.ConvertBase64ToByteArray(dto.key_project_deliverables) : null,
                evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : null
            };

            _context.HECFundedResearchProjectCompleted.Add(projectCompleted);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "HEC Funded Research Project Completed submitted successfully.", ProjectId = projectCompleted.id });
        }


        [Authorize]
        [HttpPost("submit-non-hec-funded-research-project-completed")]
        public async Task<IActionResult> SubmitNonHECFundedResearchProjectCompleted(NonHECFundedResearchProjectCompletedDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            var ricForm = await _context.ric_form_1.FindAsync(dto.ric_form_1_id);
            if (ricForm == null)
            {
                return NotFound("RicForm1 record not found.");
            }

            var projectCompleted = new NonHECFundedResearchProjectCompleted
            {
                ric_form_1_id = dto.ric_form_1_id,
                ric_form_1 = ricForm,
                research_grant_name = dto.research_grant_name,
                project_completion_date = dto.project_completion_date,
                PI_name = dto.PI_name,
                designation = dto.designation,
                department = dto.department,
                thematic_area = dto.thematic_area,
                research_proposal_title = dto.research_proposal_title,
                start_date = dto.start_date,
                end_date = dto.end_date,
                project_status = dto.project_status,
                total_funding_utilized = dto.total_funding_utilized,
                total_funding_released = dto.total_funding_released,
                key_project_deliverables = !string.IsNullOrEmpty(dto.key_project_deliverables) ? FileHelper.ConvertBase64ToByteArray(dto.key_project_deliverables) : null,
                evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : null
            };

            _context.NonHECFundedResearchProjectCompleted.Add(projectCompleted);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Non-HEC Funded Research Project Completed submitted successfully.", ProjectId = projectCompleted.id });
        }

        [Authorize]
        [HttpPost("submit-joint-research-projects")]
        public async Task<IActionResult> SubmitJointResearchProjects(JointResearchProjectsSubmittedDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            var ricForm = await _context.ric_form_1.FindAsync(dto.ric_form_1_id);
            if (ricForm == null)
            {
                return NotFound("RicForm1 record not found.");
            }

            var jointResearchProject = new JointResearchProjectsSubmitted
            {
                ric_form_1_id = dto.ric_form_1_id,
                ric_form_1 = ricForm,
                joint_research_grant_name = dto.joint_research_grant_name,
                funding_agency = dto.funding_agency,
                submission_date = dto.submission_date,
                national_or_international = dto.national_or_international,
                PI_name = dto.PI_name,
                designation = dto.designation,
                department = dto.department,
                co_PI_name = dto.co_PI_name,
                co_PI_designation = dto.co_PI_designation,
                co_PI_department = dto.co_PI_department,
                co_PI_University = dto.co_PI_University,
                thematic_area = dto.thematic_area,
                research_proposal_title = dto.research_proposal_title,
                start_date = dto.start_date,
                end_date = dto.end_date,
                total_funding_requested = dto.total_funding_requested,
                co_funding_partners_details = dto.co_funding_partners_details,
                status = dto.status,
                remarks = dto.remarks,
                evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : null
            };

            _context.JointResearchProjectsSubmitted.Add(jointResearchProject);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Joint Research Project Submitted successfully.", ProjectId = jointResearchProject.id });
        }
        [Authorize]
        [HttpPost("submit-joint-research-projects-approved")]
        public async Task<IActionResult> SubmitJointResearchProjectsApproved(JointResearchProjectsApprovedDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            var ricForm = await _context.ric_form_1.FindAsync(dto.ric_form_1_id);
            if (ricForm == null)
            {
                return NotFound("RicForm1 record not found.");
            }

            var jointResearchProjectApproved = new JointResearchProjectsApproved
            {
                ric_form_1_id = dto.ric_form_1_id,
                ric_form_1 = ricForm,
                joint_research_grant_name = dto.joint_research_grant_name,
                funding_agency = dto.funding_agency,
                approval_date = dto.approval_date,
                national_or_international = dto.national_or_international,
                PI_name = dto.PI_name,
                designation = dto.designation,
                department = dto.department,
                co_PI_name = dto.co_PI_name,
                coPI_designation = dto.coPI_designation,
                co_PI_department = dto.co_PI_department,
                co_PI_university = dto.co_PI_university,
                thematic_area = dto.thematic_area,
                research_proposal_title = dto.research_proposal_title,
                start_date = dto.start_date,
                end_date = dto.end_date,
                total_funding_approved = dto.total_funding_approved,
                co_funding_partners_details = dto.co_funding_partners_details,
                status = dto.status,
                remarks = dto.remarks,
                evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : null
            };

            _context.JointResearchProjectsApproved.Add(jointResearchProjectApproved);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Joint Research Project Approved successfully.", ProjectId = jointResearchProjectApproved.id });
        }

        [Authorize]
        [HttpPost("submit-joint-research-projects-completed")]
        public async Task<IActionResult> SubmitJointResearchProjectsCompleted(JointResearchProjectsCompletedDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            var ricForm = await _context.ric_form_1.FindAsync(dto.ric_form_1_id);
            if (ricForm == null)
            {
                return NotFound("RicForm1 record not found.");
            }

            var jointResearchProjectCompleted = new JointResearchProjectsCompleted
            {
                ric_form_1_id = dto.ric_form_1_id,
                ric_form_1 = ricForm,
                joint_research_grant_name = dto.joint_research_grant_name,
                funding_agency = dto.funding_agency,
                completion_date = dto.completion_date,
                national_or_international = dto.national_or_international,
                PI_name = dto.PI_name,
                designation = dto.designation,
                department = dto.department,
                co_PI_name = dto.co_PI_name,
                co_PI_designation = dto.co_PI_designation,
                co_PI_department = dto.co_PI_department,
                co_PI_university = dto.co_PI_university,
                thematic_area = dto.thematic_area,
                research_proposal_title = dto.research_proposal_title,
                start_date = dto.start_date,
                end_date = dto.end_date,
                total_funding_utilized = dto.total_funding_utilized,
                total_funding_requested = dto.total_funding_requested,
                co_funding_partners_details = dto.co_funding_partners_details,
                status = dto.status,
                key_project_deliverables = !string.IsNullOrEmpty(dto.key_project_deliverables) ? FileHelper.ConvertBase64ToByteArray(dto.key_project_deliverables) : null,
                evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : null
            };

            _context.JointResearchProjectsCompleted.Add(jointResearchProjectCompleted);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Joint Research Project Completed successfully.", ProjectId = jointResearchProjectCompleted.id });
        }

        [Authorize]
        [HttpPost("contract-research-awarded")]
        public async Task<IActionResult> AddContractResearchAwarded(ContractResearchAwardedDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            var ricForm = await _context.ric_form_1.FindAsync(dto.ric_form_1_id);
            if (ricForm == null)
            {
                return NotFound("RicForm1 record not found.");
            }

            var contractResearch = new ContractResearchAwarded
            {
                ric_form_1 = ricForm,
                thematic_area = dto.thematic_area,
                research_proposal_title = dto.research_proposal_title,
                contract_signed_date = dto.contract_signed_date,
                PI_name = dto.PI_name,
                PI_designation = dto.PI_designation,
                PI_department = dto.PI_department,
                co_PI_name = dto.co_PI_name,
                co_PI_designation = dto.co_PI_designation,
                co_PI_department = dto.co_PI_department,
                co_PI_university = dto.co_PI_university,
                sponsoring_agency_name = dto.sponsoring_agency_name,
                sponsoring_agency_address = dto.sponsoring_agency_address,
                sponsoring_agency_country = dto.sponsoring_agency_country,
                national_or_international = dto.national_or_international,
                counterpart_industry = dto.counterpart_industry,
                start_date = dto.start_date,
                end_date = dto.end_date,
                total_amount_approved = dto.total_amount_approved,
                expected_deliverables = dto.expected_deliverables,
                evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : null
            };

            _context.ContractResearchAwarded.Add(contractResearch);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Contract Research Awarded added successfully." });
        }

        [Authorize]
        [HttpPost("submit-policy-advocacy-case-study")]
        public async Task<IActionResult> SubmitPolicyAdvocacyCaseStudy(PolicyAdvocacyDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            var ricForm = await _context.ric_form_1.FindAsync(dto.ric_form_1_id);
            if (ricForm == null)
            {
                return NotFound("RicForm1 record not found.");
            }

            var policyAdvocacyCaseStudy = new PolicyAdvocacies
            {
                ric_form_1_id = dto.ric_form_1_id,
                ric_form_1 = ricForm,
                government_body_presented = dto.government_body_presented,
                presentation_date = dto.presentation_date,
                PI_name = dto.PI_name,
                PI_designation = dto.PI_designation,
                PI_department = dto.PI_department,
                advocacy_area = dto.advocacy_area,
                brief = dto.brief,
                start_date = dto.start_date,
                end_date = dto.end_date,
                coalition_partners = dto.coalition_partners,
                research_status = dto.research_status,
                advocacy_tools = dto.advocacy_tools,
                evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : null
            };

            _context.PolicyAdvocacies.Add(policyAdvocacyCaseStudy);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Policy Advocacy Case Study submitted successfully.", CaseStudyId = policyAdvocacyCaseStudy.id });
        }

        [Authorize]
        [HttpPost("submit-research-link")]
        public async Task<IActionResult> SubmitResearchLink(ResearchLinkageDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            var ricForm = await _context.ric_form_1.FindAsync(dto.ric_form_1_Id);
            if (ricForm == null)
            {
                return NotFound("RicForm1 record not found.");
            }

            var researchLink = new ResearchLinks
            {
                ric_form_1_Id = dto.ric_form_1_Id,
                ric_form_1 = ricForm,
                linkage_type = dto.linkage_type,
                MoU_agreement_date = dto.MoU_agreement_date,
                national_or_international = dto.national_or_international,
                host_institution_name = dto.host_institution_name,
                host_institution_address = dto.host_institution_address,
                collaborating_agency_name = dto.collaborating_agency_name,
                collaborating_agency_address = dto.collaborating_agency_address,
                field_of_study = dto.field_of_study,
                scope_of_collaboration = dto.scope_of_collaboration,
                salient_features = dto.salient_features,
                evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : null
            };

            _context.ResearchLinks.Add(researchLink);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Research link submitted successfully.", LinkId = researchLink.id });
        }

        [Authorize]
        [HttpPost("submit-civic-engagement-event")]
        public async Task<IActionResult> SubmitCivicEngagementEvent(CivicEngagementDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            var ricForm = await _context.ric_form_1.FindAsync(dto.ric_form_1_id);
            if (ricForm == null)
            {
                return NotFound("RicForm1 record not found.");
            }

            var civicEvent = new CivicEngagementEvents
            {
                ric_form_1_id = dto.ric_form_1_id,
                ric_form_1 = ricForm,
                event_title = dto.event_title,
                event_date = dto.event_date,
                community_component_involved = dto.community_component_involved,
                outcome = dto.outcome,
                collaboration_developed = dto.collaboration_developed,
                engaged_CSOs_Or_NGOs = dto.engaged_CSOs_Or_NGOs,
                sponsoring_agency = dto.sponsoring_agency,
                grant_value = dto.grant_value,
                arranged_or_participated = dto.arranged_or_participated,
                dissemination_material = dto.dissemination_material,
                remarks = dto.remarks,
                evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : null
            };

            _context.CivicEngagements.Add(civicEvent);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Civic Engagement Event submitted successfully.", EventId = civicEvent.id });
        }

        [Authorize]
        [HttpPost("submit-consultancy-contract")]
        public async Task<IActionResult> SubmitConsultancyContract(ConsultancyContractDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            var ricForm = await _context.ric_form_1.FindAsync(dto.ric_form_1_id);
            if (ricForm == null)
            {
                return NotFound("RicForm1 record not found.");
            }

            var consultancyContract = new ConsultancyContracts
            {
                ric_form_1_id = dto.ric_form_1_id,
                ric_form_1 = ricForm,
                project_title = dto.project_title,
                execution_date = dto.execution_date,
                PI_name = dto.PI_name,
                PI_designation = dto.PI_designation,
                PI_department = dto.PI_department,
                company_details = dto.company_details,
                contract_value = dto.contract_value,
                start_date = dto.start_date,
                end_date = dto.end_date,
                consultancy_type = dto.consultancy_type,
                key_deliverables = dto.key_deliverables,
                ORIC_percentage = dto.ORIC_percentage,
                remarks = dto.remarks,
                evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : null
            };

            _context.ConsultancyContracts.Add(consultancyContract);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Consultancy Contract submitted successfully.", ContractId = consultancyContract.id });
        }
        [Authorize]
        [HttpPost("submit-liaison-asrb")]
        public async Task<IActionResult> SubmitLiaisonWithASRB(LiaisonDevelopedDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            var ricForm = await _context.ric_form_1.FindAsync(dto.ric_form_1_id);
            if (ricForm == null)
            {
                return NotFound("RicForm1 record not found.");
            }

            var liaison = new LiaisonsASRB
            {
                ric_form_1_id = dto.ric_form_1_id,
                ric_form_1 = ricForm,
                liaison_with = dto.liaison_with,
                execution_date = dto.execution_date,
                evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : null
            };

            _context.LiaisonsASRB.Add(liaison);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Liaison Developed with ASRB submitted successfully.", LiaisonId = liaison.id });
        }

        [HttpGet("GetRicForm1")]
        [Authorize]
        public async Task<IActionResult> GetRicForm1()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            var departmentIdClaim = User.FindFirst("department_id")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(roleClaim))
            {
                return Unauthorized("Invalid token, missing user information.");
            }

            int userId = int.Parse(userIdClaim);
            string role = roleClaim;
            int? departmentId = string.IsNullOrEmpty(departmentIdClaim) ? null : int.Parse(departmentIdClaim);

            IQueryable<ric_form_1> query = _context.ric_form_1
                .Include(r => r.ResearchProjectSubmittedHEC)
                .Include(r => r.ResearchProjectSubmittedNonHEC)
                .Include(r => r.ResearchProjectApprovedHEC)
                .Include(r => r.ResearchProjectApprovedNonHEC)
                .Include(r => r.HECFundedResearchProjectCompleted)
                .Include(r => r.NonHECFundedResearchProjectCompleted)
                .Include(r => r.JointResearchProjectsSubmitted)
                .Include(r => r.JointResearchProjectsApproved)
                .Include(r => r.JointResearchProjectsCompleted)
                .Include(r => r.ContractResearchAwarded)
                .Include(r => r.PolicyAdvocacies)
                .Include(r => r.ResearchLinks)
                .Include(r => r.CivicEngagements)
                .Include(r => r.ConsultancyContracts)
                .Include(r => r.LiaisonsASRB);

            // Apply role-based filtering
            if (role == "dean" && departmentId.HasValue)
            {
                var departmentUsers = await _context.dataoric
                    .Where(d => d.department_id == departmentId)
                    .Select(d => d.dataoric_id)
                    .ToListAsync();

                query = query.Where(r => departmentUsers.Contains(r.dataoric_id));
            }
            else if (role == "faculty")
            {
                query = query.Where(r => r.dataoric_id == userId);
            }
            else if (role != "admin")
            {
                return Forbid();
            }

            var forms = await query.ToListAsync();

            // ✅ Convert evidence and key_project_variables fields from byte[] to Base64
            var result = forms.Select(form => new
            {
                form.ric_form_1_id,
                form.dataoric_id,

                ResearchProjectSubmittedHEC = form.ResearchProjectSubmittedHEC?.Select(p => new
                {
                    p.id,
                    Evidence = p.evidence != null ? FileHelper.ConvertByteArrayToBase64(p.evidence) : null
                }),

                ResearchProjectSubmittedNonHEC = form.ResearchProjectSubmittedNonHEC?.Select(p => new
                {
                    p.id,
                    Evidence = p.evidence != null ? FileHelper.ConvertByteArrayToBase64(p.evidence) : null
                }),

                ResearchProjectApprovedHEC = form.ResearchProjectApprovedHEC?.Select(p => new
                {
                    p.id,
                    Evidence = p.evidence != null ? FileHelper.ConvertByteArrayToBase64(p.evidence) : null
                }),

                ResearchProjectApprovedNonHEC = form.ResearchProjectApprovedNonHEC?.Select(p => new
                {
                    p.id,
                    Evidence = p.evidence != null ? FileHelper.ConvertByteArrayToBase64(p.evidence) : null
                }),

                HECFundedResearchProjectCompleted = form.HECFundedResearchProjectCompleted?.Select(p => new
                {
                    p.id,
                    Evidence = p.evidence != null ? FileHelper.ConvertByteArrayToBase64(p.evidence) : null,
                    KeyProjectdeliverabless = p.key_project_deliverables != null ? FileHelper.ConvertByteArrayToBase64(p.key_project_deliverables) : null
                }),

                NonHECFundedResearchProjectCompleted = form.NonHECFundedResearchProjectCompleted?.Select(p => new
                {
                    p.id,
                    Evidence = p.evidence != null ? FileHelper.ConvertByteArrayToBase64(p.evidence) : null,
                    KeyProjectdeliverables = p.key_project_deliverables != null ? FileHelper.ConvertByteArrayToBase64(p.key_project_deliverables) : null
                }),

                JointResearchProjectsSubmitted = form.JointResearchProjectsSubmitted?.Select(p => new
                {
                    p.id,
                    Evidence = p.evidence != null ? FileHelper.ConvertByteArrayToBase64(p.evidence) : null
                }),

                JointResearchProjectsApproved = form.JointResearchProjectsApproved?.Select(p => new
                {
                    p.id,
                    Evidence = p.evidence != null ? FileHelper.ConvertByteArrayToBase64(p.evidence) : null
                }),

                JointResearchProjectsCompleted = form.JointResearchProjectsCompleted?.Select(p => new
                {
                    p.id,
                    Evidence = p.evidence != null ? FileHelper.ConvertByteArrayToBase64(p.evidence) : null,
                    KeyProjectdeliverables = p.key_project_deliverables != null ? FileHelper.ConvertByteArrayToBase64(p.key_project_deliverables) : null
                }),

                ContractResearchAwarded = form.ContractResearchAwarded?.Select(p => new
                {
                    p.id,
                    Evidence = p.evidence != null ? FileHelper.ConvertByteArrayToBase64(p.evidence) : null
                }),

                PolicyAdvocacies = form.PolicyAdvocacies?.Select(p => new
                {
                    p.id,
                    Evidence = p.evidence != null ? FileHelper.ConvertByteArrayToBase64(p.evidence) : null
                }),

                ResearchLinks = form.ResearchLinks?.Select(p => new
                {
                    p.id,
                    Evidence = p.evidence != null ? FileHelper.ConvertByteArrayToBase64(p.evidence) : null
                }),

                CivicEngagements = form.CivicEngagements?.Select(p => new
                {
                    p.id,
                    Evidence = p.evidence != null ? FileHelper.ConvertByteArrayToBase64(p.evidence) : null
                }),

                ConsultancyContracts = form.ConsultancyContracts?.Select(p => new
                {
                    p.id,
                    Evidence = p.evidence != null ? FileHelper.ConvertByteArrayToBase64(p.evidence) : null
                }),

                LiaisonsASRB = form.LiaisonsASRB?.Select(p => new
                {
                    p.id,
                    Evidence = p.evidence != null ? FileHelper.ConvertByteArrayToBase64(p.evidence) : null
                }),
            });

            return Ok(result);
        }


        [HttpGet("GetResearchProjectSubmittedHEC")]
        [Authorize]
        public async Task<IActionResult> GetResearchProjectSubmittedHEC()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            var departmentIdClaim = User.FindFirst("department_id")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(roleClaim))
                return Unauthorized("Invalid token, missing user information.");

            int userId = int.Parse(userIdClaim);
            string role = roleClaim;
            int? departmentId = !string.IsNullOrEmpty(departmentIdClaim) ? int.Parse(departmentIdClaim) : null;

            IQueryable<ResearchProjectSubmittedHEC> query = _context.ResearchProjectSubmittedHEC
                .Include(p => p.ric_form_1);

            if (role == "dean" && departmentId.HasValue)
            {
                var departmentUsers = await _context.dataoric
                    .Where(d => d.department_id == departmentId)
                    .Select(d => d.dataoric_id)
                    .ToListAsync();

                query = query.Where(p => departmentUsers.Contains(p.ric_form_1.dataoric_id));
            }
            else if (role == "faculty")
            {
                query = query.Where(p => p.ric_form_1.dataoric_id == userId);
            }
            else if (role != "admin")
            {
                return Forbid();
            }

            var result = await query.Select(p => new
            {
                p.id,
                Evidence = p.evidence != null ? FileHelper.ConvertByteArrayToBase64(p.evidence) : null
            }).ToListAsync();

            return Ok(result);
        }

        [HttpGet("GetResearchProjectSubmittedNonHEC")]
        [Authorize]
        public async Task<IActionResult> GetResearchProjectSubmittedNonHEC()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            var departmentIdClaim = User.FindFirst("department_id")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(roleClaim))
                return Unauthorized("Invalid token, missing user information.");

            int userId = int.Parse(userIdClaim);
            string role = roleClaim;
            int? departmentId = !string.IsNullOrEmpty(departmentIdClaim) ? int.Parse(departmentIdClaim) : null;

            IQueryable<ResearchProjectSubmittedNonHEC> query = _context.ResearchProjectSubmittedNonHEC
                .Include(p => p.ric_form_1);

            if (role == "dean" && departmentId.HasValue)
            {
                var departmentUsers = await _context.dataoric
                    .Where(d => d.department_id == departmentId)
                    .Select(d => d.dataoric_id)
                    .ToListAsync();

                query = query.Where(p => departmentUsers.Contains(p.ric_form_1.dataoric_id));
            }
            else if (role == "faculty")
            {
                query = query.Where(p => p.ric_form_1.dataoric_id == userId);
            }
            else if (role != "admin")
            {
                return Forbid();
            }

            var result = await query.Select(p => new
            {
                p.id,
                Evidence = p.evidence != null ? FileHelper.ConvertByteArrayToBase64(p.evidence) : null
            }).ToListAsync();

            return Ok(result);
        }

        [HttpGet("GetHECFundedResearchProjectCompleted")]
        [Authorize]
        public async Task<IActionResult> GetHECFundedResearchProjectCompleted()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            var departmentIdClaim = User.FindFirst("department_id")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(roleClaim))
                return Unauthorized("Invalid token, missing user information.");

            int userId = int.Parse(userIdClaim);
            string role = roleClaim;
            int? departmentId = !string.IsNullOrEmpty(departmentIdClaim) ? int.Parse(departmentIdClaim) : null;

            IQueryable<HECFundedResearchProjectCompleted> query = _context.HECFundedResearchProjectCompleted
                .Include(p => p.ric_form_1);

            if (role == "dean" && departmentId.HasValue)
            {
                var departmentUsers = await _context.dataoric
                    .Where(d => d.department_id == departmentId)
                    .Select(d => d.dataoric_id)
                    .ToListAsync();

                query = query.Where(p => departmentUsers.Contains(p.ric_form_1.dataoric_id));
            }
            else if (role == "faculty")
            {
                query = query.Where(p => p.ric_form_1.dataoric_id == userId);
            }
            else if (role != "admin")
            {
                return Forbid();
            }

            var result = await query.Select(p => new
            {
                p.id,
                Evidence = p.evidence != null ? FileHelper.ConvertByteArrayToBase64(p.evidence) : null,
                KeyProjectdeliverables = p.key_project_deliverables != null ? FileHelper.ConvertByteArrayToBase64(p.key_project_deliverables) : null
            }).ToListAsync();

            return Ok(result);
        }

        [HttpGet("GetNONHECFundedResearchProjectCompleted")]
        [Authorize]
        public async Task<IActionResult> GetNONHECFundedResearchProjectCompleted()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            var departmentIdClaim = User.FindFirst("department_id")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(roleClaim))
                return Unauthorized("Invalid token, missing user information.");

            int userId = int.Parse(userIdClaim);
            string role = roleClaim;
            int? departmentId = !string.IsNullOrEmpty(departmentIdClaim) ? int.Parse(departmentIdClaim) : null;

            IQueryable<HECFundedResearchProjectCompleted> query = _context.HECFundedResearchProjectCompleted
                .Include(p => p.ric_form_1);

            if (role == "dean" && departmentId.HasValue)
            {
                var departmentUsers = await _context.dataoric
                    .Where(d => d.department_id == departmentId)
                    .Select(d => d.dataoric_id)
                    .ToListAsync();

                query = query.Where(p => departmentUsers.Contains(p.ric_form_1.dataoric_id));
            }
            else if (role == "faculty")
            {
                query = query.Where(p => p.ric_form_1.dataoric_id == userId);
            }
            else if (role != "admin")
            {
                return Forbid();
            }

            var result = await query.Select(p => new
            {
                p.id,
                Evidence = p.evidence != null ? FileHelper.ConvertByteArrayToBase64(p.evidence) : null,
                KeyProjectdeliverables = p.key_project_deliverables != null ? FileHelper.ConvertByteArrayToBase64(p.key_project_deliverables) : null
            }).ToListAsync();

            return Ok(result);
        }

        [HttpGet("GetJointResearchProjectCompleted")]
        [Authorize]
        public async Task<IActionResult> GetJointResearchProjectCompleted()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            var departmentIdClaim = User.FindFirst("department_id")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(roleClaim))
                return Unauthorized("Invalid token, missing user information.");

            int userId = int.Parse(userIdClaim);
            string role = roleClaim;
            int? departmentId = !string.IsNullOrEmpty(departmentIdClaim) ? int.Parse(departmentIdClaim) : null;

            IQueryable<HECFundedResearchProjectCompleted> query = _context.HECFundedResearchProjectCompleted
                .Include(p => p.ric_form_1);

            if (role == "dean" && departmentId.HasValue)
            {
                var departmentUsers = await _context.dataoric
                    .Where(d => d.department_id == departmentId)
                    .Select(d => d.dataoric_id)
                    .ToListAsync();

                query = query.Where(p => departmentUsers.Contains(p.ric_form_1.dataoric_id));
            }
            else if (role == "faculty")
            {
                query = query.Where(p => p.ric_form_1.dataoric_id == userId);
            }
            else if (role != "admin")
            {
                return Forbid();
            }

            var result = await query.Select(p => new
            {
                p.id,
                Evidence = p.evidence != null ? FileHelper.ConvertByteArrayToBase64(p.evidence) : null,
                KeyProjectdeliverables = p.key_project_deliverables != null ? FileHelper.ConvertByteArrayToBase64(p.key_project_deliverables) : null
            }).ToListAsync();

            return Ok(result);
        }

        [HttpGet("GetResearchProjectApprovedHEC")]
        [Authorize]
        public async Task<IActionResult> GetResearchProjectApprovedHEC()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            var departmentIdClaim = User.FindFirst("department_id")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(roleClaim))
                return Unauthorized("Invalid token, missing user information.");

            int userId = int.Parse(userIdClaim);
            string role = roleClaim;
            int? departmentId = !string.IsNullOrEmpty(departmentIdClaim) ? int.Parse(departmentIdClaim) : null;

            IQueryable<ResearchProjectApprovedHEC> query = _context.ResearchProjectApprovedHEC
                .Include(p => p.ric_form_1);

            if (role == "dean" && departmentId.HasValue)
            {
                var departmentUsers = await _context.dataoric
                    .Where(d => d.department_id == departmentId)
                    .Select(d => d.dataoric_id)
                    .ToListAsync();

                query = query.Where(p => departmentUsers.Contains(p.ric_form_1.dataoric_id));
            }
            else if (role == "faculty")
            {
                query = query.Where(p => p.ric_form_1.dataoric_id == userId);
            }
            else if (role != "admin")
            {
                return Forbid();
            }

            var result = await query.Select(p => new
            {
                p.id,
                Evidence = p.evidence != null ? FileHelper.ConvertByteArrayToBase64(p.evidence) : null
            }).ToListAsync();

            return Ok(result);
        }

        [HttpGet("GetResearchProjectApprovedNonHEC")]
        [Authorize]
        public async Task<IActionResult> GetResearchProjectApprovedNonHEC()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            var departmentIdClaim = User.FindFirst("department_id")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(roleClaim))
                return Unauthorized("Invalid token, missing user information.");

            int userId = int.Parse(userIdClaim);
            string role = roleClaim;
            int? departmentId = !string.IsNullOrEmpty(departmentIdClaim) ? int.Parse(departmentIdClaim) : null;

            IQueryable<ResearchProjectApprovedHEC> query = _context.ResearchProjectApprovedHEC
                .Include(p => p.ric_form_1);

            if (role == "dean" && departmentId.HasValue)
            {
                var departmentUsers = await _context.dataoric
                    .Where(d => d.department_id == departmentId)
                    .Select(d => d.dataoric_id)
                    .ToListAsync();

                query = query.Where(p => departmentUsers.Contains(p.ric_form_1.dataoric_id));
            }
            else if (role == "faculty")
            {
                query = query.Where(p => p.ric_form_1.dataoric_id == userId);
            }
            else if (role != "admin")
            {
                return Forbid();
            }

            var result = await query.Select(p => new
            {
                p.id,
                Evidence = p.evidence != null ? FileHelper.ConvertByteArrayToBase64(p.evidence) : null
            }).ToListAsync();

            return Ok(result);
        }
        [HttpGet("GetJointResearchProjectsSubmitted")]
        [Authorize]
        public async Task<IActionResult> GetJointResearchProjectsSubmitted()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            var departmentIdClaim = User.FindFirst("department_id")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(roleClaim))
                return Unauthorized("Invalid token, missing user information.");

            int userId = int.Parse(userIdClaim);
            string role = roleClaim;
            int? departmentId = !string.IsNullOrEmpty(departmentIdClaim) ? int.Parse(departmentIdClaim) : null;

            IQueryable<JointResearchProjectsSubmitted> query = _context.JointResearchProjectsSubmitted
                .Include(p => p.ric_form_1);

            if (role == "dean" && departmentId.HasValue)
            {
                var departmentUsers = await _context.dataoric
                    .Where(d => d.department_id == departmentId)
                    .Select(d => d.dataoric_id)
                    .ToListAsync();

                query = query.Where(p => departmentUsers.Contains(p.ric_form_1.dataoric_id));
            }
            else if (role == "faculty")
            {
                query = query.Where(p => p.ric_form_1.dataoric_id == userId);
            }
            else if (role != "admin")
            {
                return Forbid();
            }

            var result = await query.Select(p => new
            {
                p.id,
                Evidence = p.evidence != null ? FileHelper.ConvertByteArrayToBase64(p.evidence) : null
            }).ToListAsync();

            return Ok(result);
        }
        [HttpGet("GetJointResearchProjectsApproved")]
        [Authorize]
        public async Task<IActionResult> GetJointResearchProjectsApproved()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            var departmentIdClaim = User.FindFirst("department_id")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(roleClaim))
                return Unauthorized("Invalid token, missing user information.");

            int userId = int.Parse(userIdClaim);
            string role = roleClaim;
            int? departmentId = !string.IsNullOrEmpty(departmentIdClaim) ? int.Parse(departmentIdClaim) : null;

            IQueryable<JointResearchProjectsSubmitted> query = _context.JointResearchProjectsSubmitted
                .Include(p => p.ric_form_1);

            if (role == "dean" && departmentId.HasValue)
            {
                var departmentUsers = await _context.dataoric
                    .Where(d => d.department_id == departmentId)
                    .Select(d => d.dataoric_id)
                    .ToListAsync();

                query = query.Where(p => departmentUsers.Contains(p.ric_form_1.dataoric_id));
            }
            else if (role == "faculty")
            {
                query = query.Where(p => p.ric_form_1.dataoric_id == userId);
            }
            else if (role != "admin")
            {
                return Forbid();
            }

            var result = await query.Select(p => new
            {
                p.id,
                Evidence = p.evidence != null ? FileHelper.ConvertByteArrayToBase64(p.evidence) : null
            }).ToListAsync();

            return Ok(result);
        }
        [HttpGet("GetContractResearchAwarded")]
        [Authorize]
        public async Task<IActionResult> GetContractResearchAwarded()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            var departmentIdClaim = User.FindFirst("department_id")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(roleClaim))
                return Unauthorized("Invalid token, missing user information.");

            int userId = int.Parse(userIdClaim);
            string role = roleClaim;
            int? departmentId = !string.IsNullOrEmpty(departmentIdClaim) ? int.Parse(departmentIdClaim) : null;

            IQueryable<JointResearchProjectsSubmitted> query = _context.JointResearchProjectsSubmitted
                .Include(p => p.ric_form_1);

            if (role == "dean" && departmentId.HasValue)
            {
                var departmentUsers = await _context.dataoric
                    .Where(d => d.department_id == departmentId)
                    .Select(d => d.dataoric_id)
                    .ToListAsync();

                query = query.Where(p => departmentUsers.Contains(p.ric_form_1.dataoric_id));
            }
            else if (role == "faculty")
            {
                query = query.Where(p => p.ric_form_1.dataoric_id == userId);
            }
            else if (role != "admin")
            {
                return Forbid();
            }

            var result = await query.Select(p => new
            {
                p.id,
                Evidence = p.evidence != null ? FileHelper.ConvertByteArrayToBase64(p.evidence) : null
            }).ToListAsync();

            return Ok(result);
        }
        [HttpGet("GetPolicyAdvocacyCaseStudies")]
        [Authorize]
        public async Task<IActionResult> GetPolicyAdvocacyCaseStudies()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            var departmentIdClaim = User.FindFirst("department_id")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(roleClaim))
                return Unauthorized("Invalid token, missing user information.");

            int userId = int.Parse(userIdClaim);
            string role = roleClaim;
            int? departmentId = !string.IsNullOrEmpty(departmentIdClaim) ? int.Parse(departmentIdClaim) : null;

            IQueryable<PolicyAdvocacies> query = _context.PolicyAdvocacies
                .Include(p => p.ric_form_1);

            if (role == "dean" && departmentId.HasValue)
            {
                var departmentUsers = await _context.dataoric
                    .Where(d => d.department_id == departmentId)
                    .Select(d => d.dataoric_id)
                    .ToListAsync();

                query = query.Where(p => departmentUsers.Contains(p.ric_form_1.dataoric_id));
            }
            else if (role == "faculty")
            {
                query = query.Where(p => p.ric_form_1.dataoric_id == userId);
            }
            else if (role != "admin")
            {
                return Forbid();
            }

            var result = await query.Select(p => new
            {
                p.id,
                Evidence = p.evidence != null ? FileHelper.ConvertByteArrayToBase64(p.evidence) : null
            }).ToListAsync();

            return Ok(result);
        }

        [HttpGet("GetResearchLinks")]
        [Authorize]
        public async Task<IActionResult> GetResearchLinks()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            var departmentIdClaim = User.FindFirst("department_id")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(roleClaim))
                return Unauthorized("Invalid token, missing user information.");

            int userId = int.Parse(userIdClaim);
            string role = roleClaim;
            int? departmentId = !string.IsNullOrEmpty(departmentIdClaim) ? int.Parse(departmentIdClaim) : null;

            IQueryable<PolicyAdvocacies> query = _context.PolicyAdvocacies
                .Include(p => p.ric_form_1);

            if (role == "dean" && departmentId.HasValue)
            {
                var departmentUsers = await _context.dataoric
                    .Where(d => d.department_id == departmentId)
                    .Select(d => d.dataoric_id)
                    .ToListAsync();

                query = query.Where(p => departmentUsers.Contains(p.ric_form_1.dataoric_id));
            }
            else if (role == "faculty")
            {
                query = query.Where(p => p.ric_form_1.dataoric_id == userId);
            }
            else if (role != "admin")
            {
                return Forbid();
            }

            var result = await query.Select(p => new
            {
                p.id,
                Evidence = p.evidence != null ? FileHelper.ConvertByteArrayToBase64(p.evidence) : null
            }).ToListAsync();

            return Ok(result);
        }

        [HttpGet("GetCivicEngagements")]
        [Authorize]
        public async Task<IActionResult> GetCivicEngagements()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            var departmentIdClaim = User.FindFirst("department_id")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(roleClaim))
                return Unauthorized("Invalid token, missing user information.");

            int userId = int.Parse(userIdClaim);
            string role = roleClaim;
            int? departmentId = !string.IsNullOrEmpty(departmentIdClaim) ? int.Parse(departmentIdClaim) : null;

            IQueryable<PolicyAdvocacies> query = _context.PolicyAdvocacies
                .Include(p => p.ric_form_1);

            if (role == "dean" && departmentId.HasValue)
            {
                var departmentUsers = await _context.dataoric
                    .Where(d => d.department_id == departmentId)
                    .Select(d => d.dataoric_id)
                    .ToListAsync();

                query = query.Where(p => departmentUsers.Contains(p.ric_form_1.dataoric_id));
            }
            else if (role == "faculty")
            {
                query = query.Where(p => p.ric_form_1.dataoric_id == userId);
            }
            else if (role != "admin")
            {
                return Forbid();
            }

            var result = await query.Select(p => new
            {
                p.id,
                Evidence = p.evidence != null ? FileHelper.ConvertByteArrayToBase64(p.evidence) : null
            }).ToListAsync();

            return Ok(result);
        }

        [HttpGet("GetConsultancyContracts")]
        [Authorize]
        public async Task<IActionResult> GetConsultancyContracts()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            var departmentIdClaim = User.FindFirst("department_id")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(roleClaim))
                return Unauthorized("Invalid token, missing user information.");

            int userId = int.Parse(userIdClaim);
            string role = roleClaim;
            int? departmentId = !string.IsNullOrEmpty(departmentIdClaim) ? int.Parse(departmentIdClaim) : null;

            IQueryable<PolicyAdvocacies> query = _context.PolicyAdvocacies
                .Include(p => p.ric_form_1);

            if (role == "dean" && departmentId.HasValue)
            {
                var departmentUsers = await _context.dataoric
                    .Where(d => d.department_id == departmentId)
                    .Select(d => d.dataoric_id)
                    .ToListAsync();

                query = query.Where(p => departmentUsers.Contains(p.ric_form_1.dataoric_id));
            }
            else if (role == "faculty")
            {
                query = query.Where(p => p.ric_form_1.dataoric_id == userId);
            }
            else if (role != "admin")
            {
                return Forbid();
            }

            var result = await query.Select(p => new
            {
                p.id,
                Evidence = p.evidence != null ? FileHelper.ConvertByteArrayToBase64(p.evidence) : null
            }).ToListAsync();

            return Ok(result);
        }

        [HttpGet("GetLiaisonsASRB")]
        [Authorize]
        public async Task<IActionResult> GetLiaisonsASRB()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            var departmentIdClaim = User.FindFirst("department_id")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(roleClaim))
                return Unauthorized("Invalid token, missing user information.");

            int userId = int.Parse(userIdClaim);
            string role = roleClaim;
            int? departmentId = !string.IsNullOrEmpty(departmentIdClaim) ? int.Parse(departmentIdClaim) : null;

            IQueryable<PolicyAdvocacies> query = _context.PolicyAdvocacies
                .Include(p => p.ric_form_1);

            if (role == "dean" && departmentId.HasValue)
            {
                var departmentUsers = await _context.dataoric
                    .Where(d => d.department_id == departmentId)
                    .Select(d => d.dataoric_id)
                    .ToListAsync();

                query = query.Where(p => departmentUsers.Contains(p.ric_form_1.dataoric_id));
            }
            else if (role == "faculty")
            {
                query = query.Where(p => p.ric_form_1.dataoric_id == userId);
            }
            else if (role != "admin")
            {
                return Forbid();
            }

            var result = await query.Select(p => new
            {
                p.id,
                Evidence = p.evidence != null ? FileHelper.ConvertByteArrayToBase64(p.evidence) : null
            }).ToListAsync();

            return Ok(result);
        }



        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateRicForm1(int id, ric_form_1Dto dto)
        {
            var ricForm = await _context.ric_form_1.FindAsync(id);
            if (ricForm == null)
            {
                return NotFound("RIC Form 1 record not found.");
            }

            // Update fields only if new values are provided
            ricForm.faculty_name = dto.faculty_name ?? ricForm.faculty_name;
            ricForm.department_name = dto.department_name ?? ricForm.department_name;
            ricForm.faculty_email = dto.faculty_email ?? ricForm.faculty_email;
            ricForm.research_grants_submitted_hec = dto.research_grants_submitted_hec;
            ricForm.research_grants_submitted_non_hec = dto.research_grants_submitted_non_hec;
            ricForm.research_grants_approved_hec = dto.research_grants_approved_hec;
            ricForm.research_grants_approved_non_hec = dto.research_grants_approved_non_hec;
            ricForm.hec_funded_projects_completed = dto.hec_funded_projects_completed;
            ricForm.non_hec_funded_projects_completed = dto.non_hec_funded_projects_completed;
            ricForm.joint_projects_submitted = dto.joint_projects_submitted;
            ricForm.joint_projects_approved = dto.joint_projects_approved;
            ricForm.joint_projects_completed = dto.joint_projects_completed;
            ricForm.policy_advocacy_case_studies = dto.policy_advocacy_case_studies;
            ricForm.research_links_established = dto.research_links_established;
            ricForm.civic_engagements = dto.civic_engagements;
            ricForm.consultancy_contracts_executed = dto.consultancy_contracts_executed;
            ricForm.liaison_with_asrb = dto.liaison_with_asrb;

            await _context.SaveChangesAsync();
            return Ok(new { Message = "RIC Form 1 updated successfully." });
        }

        [Authorize]
        [HttpPut("update-research-project-submitted-hec/{id}")]
        public async Task<IActionResult> UpdateResearchProjectSubmittedHEC(int id, ResearchProjectSubmittedHECDTO dto)
        {
            var project = await _context.ResearchProjectSubmittedHEC.FindAsync(id);
            if (project == null)
            {
                return NotFound("Research Project (HEC) not found.");
            }

            project.research_grant_name = dto.research_grant_name ?? project.research_grant_name;
            project.proposal_submission_date = dto.proposal_submission_date ?? project.proposal_submission_date;
            project.PI_name = dto.PI_name ?? project.PI_name;
            project.designation = dto.designation ?? project.designation;
            project.department = dto.department ?? project.department;
            project.thematic_area = dto.thematic_area ?? project.thematic_area;
            project.research_proposal_title = dto.research_proposal_title ?? project.research_proposal_title;
            project.start_date = dto.start_date ?? project.start_date;
            project.end_date = dto.end_date ?? project.end_date;
            project.total_funding_requested = dto.total_funding_requested ?? project.total_funding_requested;
            project.collaborating_partners = dto.collaborating_partners ?? project.collaborating_partners;
            project.co_funding_partners = dto.co_funding_partners ?? project.co_funding_partners;
            project.status = dto.status ?? project.status;
            project.remarks = dto.remarks ?? project.remarks;
            project.evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : project.evidence;

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Research Project (HEC) updated successfully." });
        }

        [Authorize]
        [HttpPut("update-research-project-submitted-non-hec/{id}")]
        public async Task<IActionResult> UpdateResearchProjectSubmittedNonHEC(int id, ResearchProjectSubmittedNonHECDTO dto)
        {
            var project = await _context.ResearchProjectSubmittedNonHEC.FindAsync(id);
            if (project == null)
            {
                return NotFound("Research Project (Non-HEC) not found.");
            }

            project.research_grant_name = dto.research_grant_name ?? project.research_grant_name;
            project.proposal_submission_date = dto.proposal_submission_date ?? project.proposal_submission_date;
            project.national_or_international = dto.national_or_international ?? project.national_or_international;
            project.PI_name = dto.PI_name ?? project.PI_name;
            project.designation = dto.designation ?? project.designation;
            project.department = dto.department ?? project.department;
            project.thematic_area = dto.thematic_area ?? project.thematic_area;
            project.research_proposal_title = dto.research_proposal_title ?? project.research_proposal_title;
            project.start_date = dto.start_date ?? project.start_date;
            project.end_date = dto.end_date ?? project.end_date;
            project.total_funding_requested = dto.total_funding_requested ?? project.total_funding_requested;
            project.collaborating_partners = dto.collaborating_partners ?? project.collaborating_partners;
            project.co_funding_partners = dto.co_funding_partners ?? project.co_funding_partners;
            project.status = dto.status ?? project.status;
            project.remarks = dto.remarks ?? project.remarks;
            project.evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : project.evidence;

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Research Project (Non-HEC) updated successfully." });
        }
        [Authorize]
        [HttpPut("update-research-project-approved-hec/{id}")]
        public async Task<IActionResult> UpdateResearchProjectApprovedHEC(int id, ResearchProjectApprovedHECDTO dto)
        {
            var project = await _context.ResearchProjectApprovedHEC.FindAsync(id);
            if (project == null)
            {
                return NotFound("Research Project Approved (HEC) not found.");
            }

            project.research_grant_name = dto.research_grant_name ?? project.research_grant_name;
            project.proposal_approval_date = dto.proposal_approval_date ?? project.proposal_approval_date;
            project.national_or_international = dto.national_or_international ?? project.national_or_international;
            project.PI_name = dto.PI_name ?? project.PI_name;
            project.designation = dto.designation ?? project.designation;
            project.department = dto.department ?? project.department;
            project.thematic_area = dto.thematic_area ?? project.thematic_area;
            project.research_proposal_title = dto.research_proposal_title ?? project.research_proposal_title;
            project.start_date = dto.start_date ?? project.start_date;
            project.end_date = dto.end_date ?? project.end_date;
            project.total_funding_approved = dto.total_funding_approved ?? project.total_funding_approved;
            project.collaborating_partners = dto.collaborating_partners ?? project.collaborating_partners;
            project.co_funding_partners = dto.co_funding_partners ?? project.co_funding_partners;
            project.approval_date = dto.approval_date ?? project.approval_date;
            project.evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : project.evidence;

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Research Project Approved (HEC) updated successfully." });
        }
        [Authorize]
        [HttpPut("update-research-project-approved-non-hec/{id}")]
        public async Task<IActionResult> UpdateResearchProjectApprovedNonHEC(int id, ResearchProjectApprovedNonHECDTO dto)
        {
            var project = await _context.ResearchProjectApprovedNonHEC.FindAsync(id);
            if (project == null)
            {
                return NotFound("Research Project Approved (Non-HEC) not found.");
            }

            project.research_grant_name = dto.research_grant_name ?? project.research_grant_name;
            project.proposal_approval_date = dto.proposal_approval_date ?? project.proposal_approval_date;
            project.national_or_international = dto.national_or_international ?? project.national_or_international;
            project.PI_name = dto.PI_name ?? project.PI_name;
            project.designation = dto.designation ?? project.designation;
            project.department = dto.department ?? project.department;
            project.thematic_area = dto.thematic_area ?? project.thematic_area;
            project.research_proposal_title = dto.research_proposal_title ?? project.research_proposal_title;
            project.start_date = dto.start_date ?? project.start_date;
            project.end_date = dto.end_date ?? project.end_date;
            project.total_funding_approved = dto.total_funding_approved ?? project.total_funding_approved;
            project.collaborating_partners = dto.collaborating_partners ?? project.collaborating_partners;
            project.co_funding_partners = dto.co_funding_partners ?? project.co_funding_partners;
            project.approval_date = dto.approval_date ?? project.approval_date;
            project.evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : project.evidence;

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Research Project Approved (Non-HEC) updated successfully." });
        }
        [Authorize]
        [HttpPut("update-hec-funded-research-project-completed/{id}")]
        public async Task<IActionResult> UpdateHECFundedResearchProjectCompleted(int id, HECFundedResearchProjectCompletedDTO dto)
        {
            var project = await _context.HECFundedResearchProjectCompleted.FindAsync(id);
            if (project == null)
            {
                return NotFound("HEC Funded Research Project not found.");
            }

            project.research_grant_name = dto.research_grant_name;
            project.project_completion_date = dto.project_completion_date;
            project.PI_name = dto.PI_name;
            project.designation = dto.designation;
            project.department = dto.department;
            project.thematic_area = dto.thematic_area;
            project.research_proposal_title = dto.research_proposal_title;
            project.start_date = dto.start_date;
            project.end_date = dto.end_date;
            project.project_status = dto.project_status;
            project.total_funding_approved = dto.total_funding_approved;
            project.total_funding_released = dto.total_funding_released;
            project.key_project_deliverables = !string.IsNullOrEmpty(dto.key_project_deliverables) ? FileHelper.ConvertBase64ToByteArray(dto.key_project_deliverables) : project.key_project_deliverables;
            project.evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : project.evidence;

            await _context.SaveChangesAsync();

            return Ok(new { Message = "HEC Funded Research Project updated successfully.", ProjectId = project.id });
        }
        [Authorize]
        [HttpPut("update-non-hec-funded-research-project-completed/{id}")]
        public async Task<IActionResult> UpdateNonHECFundedResearchProjectCompleted(int id, NonHECFundedResearchProjectCompletedDTO dto)
        {
            var project = await _context.NonHECFundedResearchProjectCompleted.FindAsync(id);
            if (project == null)
            {
                return NotFound("Non-HEC Funded Research Project not found.");
            }

            project.research_grant_name = dto.research_grant_name;
            project.project_completion_date = dto.project_completion_date;
            project.PI_name = dto.PI_name;
            project.designation = dto.designation;
            project.department = dto.department;
            project.thematic_area = dto.thematic_area;
            project.research_proposal_title = dto.research_proposal_title;
            project.start_date = dto.start_date;
            project.end_date = dto.end_date;
            project.project_status = dto.project_status;
            project.total_funding_utilized = dto.total_funding_utilized;
            project.total_funding_released = dto.total_funding_released;
            project.key_project_deliverables = !string.IsNullOrEmpty(dto.key_project_deliverables) ? FileHelper.ConvertBase64ToByteArray(dto.key_project_deliverables) : project.key_project_deliverables;
            project.evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : project.evidence;

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Non-HEC Funded Research Project updated successfully.", ProjectId = project.id });
        }
        [Authorize]
        [HttpPut("update-joint-research-projects/{id}")]
        public async Task<IActionResult> UpdateJointResearchProjects(int id, JointResearchProjectsSubmittedDTO dto)
        {
            var project = await _context.JointResearchProjectsSubmitted.FindAsync(id);
            if (project == null)
            {
                return NotFound("Joint Research Project not found.");
            }

            project.joint_research_grant_name = dto.joint_research_grant_name;
            project.funding_agency = dto.funding_agency;
            project.submission_date = dto.submission_date;
            project.national_or_international = dto.national_or_international;
            project.PI_name = dto.PI_name;
            project.designation = dto.designation;
            project.department = dto.department;
            project.co_PI_name = dto.co_PI_name;
            project.co_PI_designation = dto.co_PI_designation;
            project.co_PI_department = dto.co_PI_department;
            project.co_PI_University = dto.co_PI_University;
            project.thematic_area = dto.thematic_area;
            project.research_proposal_title = dto.research_proposal_title;
            project.start_date = dto.start_date;
            project.end_date = dto.end_date;
            project.total_funding_requested = dto.total_funding_requested;
            project.co_funding_partners_details = dto.co_funding_partners_details;
            project.status = dto.status;
            project.remarks = dto.remarks;
            project.evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : project.evidence;

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Joint Research Project updated successfully.", ProjectId = project.id });
        }
        [Authorize]
        [HttpPut("update-joint-research-projects-approved/{id}")]
        public async Task<IActionResult> UpdateJointResearchProjectsApproved(int id, JointResearchProjectsApprovedDTO dto)
        {
            var project = await _context.JointResearchProjectsApproved.FindAsync(id);
            if (project == null)
            {
                return NotFound("Joint Research Project Approved record not found.");
            }

            project.joint_research_grant_name = dto.joint_research_grant_name;
            project.funding_agency = dto.funding_agency;
            project.approval_date = dto.approval_date;
            project.national_or_international = dto.national_or_international;
            project.PI_name = dto.PI_name;
            project.designation = dto.designation;
            project.department = dto.department;
            project.co_PI_name = dto.co_PI_name;
            project.coPI_designation = dto.coPI_designation;
            project.co_PI_department = dto.co_PI_department;
            project.co_PI_university = dto.co_PI_university;
            project.thematic_area = dto.thematic_area;
            project.research_proposal_title = dto.research_proposal_title;
            project.start_date = dto.start_date;
            project.end_date = dto.end_date;
            project.total_funding_approved = dto.total_funding_approved;
            project.co_funding_partners_details = dto.co_funding_partners_details;
            project.status = dto.status;
            project.remarks = dto.remarks;
            project.evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : project.evidence;

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Joint Research Project Approved updated successfully.", ProjectId = project.id });
        }
        [Authorize]
        [HttpPut("update-joint-research-projects-completed/{id}")]
        public async Task<IActionResult> UpdateJointResearchProjectsCompleted(int id, JointResearchProjectsCompletedDTO dto)
        {
            var project = await _context.JointResearchProjectsCompleted.FindAsync(id);
            if (project == null)
            {
                return NotFound("Joint Research Project Completed record not found.");
            }

            project.joint_research_grant_name = dto.joint_research_grant_name;
            project.funding_agency = dto.funding_agency;
            project.completion_date = dto.completion_date;
            project.national_or_international = dto.national_or_international;
            project.PI_name = dto.PI_name;
            project.designation = dto.designation;
            project.department = dto.department;
            project.co_PI_name = dto.co_PI_name;
            project.co_PI_designation = dto.co_PI_designation;
            project.co_PI_department = dto.co_PI_department;
            project.co_PI_university = dto.co_PI_university;
            project.thematic_area = dto.thematic_area;
            project.research_proposal_title = dto.research_proposal_title;
            project.start_date = dto.start_date;
            project.end_date = dto.end_date;
            project.total_funding_utilized = dto.total_funding_utilized;
            project.total_funding_requested = dto.total_funding_requested;
            project.co_funding_partners_details = dto.co_funding_partners_details;
            project.status = dto.status;
            project.key_project_deliverables = !string.IsNullOrEmpty(dto.key_project_deliverables) ? FileHelper.ConvertBase64ToByteArray(dto.key_project_deliverables) : project.key_project_deliverables;
            project.evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : project.evidence;

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Joint Research Project Completed updated successfully.", ProjectId = project.id });
        }

        [Authorize]
        [HttpPut("update-contract-research-awarded/{id}")]
        public async Task<IActionResult> UpdateContractResearchAwarded(int id, ContractResearchAwardedDTO dto)
        {
            var contractResearch = await _context.ContractResearchAwarded.FindAsync(id);
            if (contractResearch == null)
            {
                return NotFound("Contract Research Awarded record not found.");
            }

            contractResearch.thematic_area = dto.thematic_area ?? contractResearch.thematic_area;
            contractResearch.research_proposal_title = dto.research_proposal_title ?? contractResearch.research_proposal_title;
            contractResearch.contract_signed_date = dto.contract_signed_date ?? contractResearch.contract_signed_date;
            contractResearch.PI_name = dto.PI_name ?? contractResearch.PI_name;
            contractResearch.PI_designation = dto.PI_designation ?? contractResearch.PI_designation;
            contractResearch.PI_department = dto.PI_department ?? contractResearch.PI_department;
            contractResearch.co_PI_name = dto.co_PI_name ?? contractResearch.co_PI_name;
            contractResearch.co_PI_designation = dto.co_PI_designation ?? contractResearch.co_PI_designation;
            contractResearch.co_PI_department = dto.co_PI_department ?? contractResearch.co_PI_department;
            contractResearch.co_PI_university = dto.co_PI_university ?? contractResearch.co_PI_university;
            contractResearch.sponsoring_agency_name = dto.sponsoring_agency_name ?? contractResearch.sponsoring_agency_name;
            contractResearch.sponsoring_agency_address = dto.sponsoring_agency_address ?? contractResearch.sponsoring_agency_address;
            contractResearch.sponsoring_agency_country = dto.sponsoring_agency_country ?? contractResearch.sponsoring_agency_country;
            contractResearch.national_or_international = dto.national_or_international ?? contractResearch.national_or_international;
            contractResearch.counterpart_industry = dto.counterpart_industry ?? contractResearch.counterpart_industry;
            contractResearch.start_date = dto.start_date ?? contractResearch.start_date;
            contractResearch.end_date = dto.end_date ?? contractResearch.end_date;
            contractResearch.total_amount_approved = dto.total_amount_approved ?? contractResearch.total_amount_approved;
            contractResearch.expected_deliverables = dto.expected_deliverables ?? contractResearch.expected_deliverables;
            contractResearch.evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : contractResearch.evidence;

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Contract Research Awarded updated successfully." });
        }


        [Authorize]
        [HttpPut("update-policy-advocacy-case-study/{id}")]
        public async Task<IActionResult> UpdatePolicyAdvocacyCaseStudy(int id, PolicyAdvocacyDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            var caseStudy = await _context.PolicyAdvocacies.FindAsync(id);
            if (caseStudy == null)
            {
                return NotFound("Policy Advocacy Case Study not found.");
            }

            caseStudy.government_body_presented = dto.government_body_presented;
            caseStudy.presentation_date = dto.presentation_date;
            caseStudy.PI_name = dto.PI_name;
            caseStudy.PI_designation = dto.PI_designation;
            caseStudy.PI_department = dto.PI_department;
            caseStudy.advocacy_area = dto.advocacy_area;
            caseStudy.brief = dto.brief;
            caseStudy.start_date = dto.start_date;
            caseStudy.end_date = dto.end_date;
            caseStudy.coalition_partners = dto.coalition_partners;
            caseStudy.research_status = dto.research_status;
            caseStudy.advocacy_tools = dto.advocacy_tools;
            caseStudy.evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : caseStudy.evidence;

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Policy Advocacy Case Study updated successfully." });
        }
        [Authorize]
        [HttpPut("update-research-link/{id}")]
        public async Task<IActionResult> UpdateResearchLink(int id, ResearchLinkageDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            var researchLink = await _context.ResearchLinks.FindAsync(id);
            if (researchLink == null)
            {
                return NotFound("Research Link not found.");
            }

            researchLink.linkage_type = dto.linkage_type;
            researchLink.MoU_agreement_date = dto.MoU_agreement_date;
            researchLink.national_or_international = dto.national_or_international;
            researchLink.host_institution_name = dto.host_institution_name;
            researchLink.host_institution_address = dto.host_institution_address;
            researchLink.collaborating_agency_name = dto.collaborating_agency_name;
            researchLink.collaborating_agency_address = dto.collaborating_agency_address;
            researchLink.field_of_study = dto.field_of_study;
            researchLink.scope_of_collaboration = dto.scope_of_collaboration;
            researchLink.salient_features = dto.salient_features;
            researchLink.evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : researchLink.evidence;

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Research Link updated successfully." });
        }
        [Authorize]
        [HttpPut("update-civic-engagement-event/{id}")]
        public async Task<IActionResult> UpdateCivicEngagementEvent(int id, CivicEngagementDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            var civicEvent = await _context.CivicEngagements.FindAsync(id);
            if (civicEvent == null)
            {
                return NotFound("Civic Engagement Event not found.");
            }

            civicEvent.event_title = dto.event_title;
            civicEvent.event_date = dto.event_date;
            civicEvent.community_component_involved = dto.community_component_involved;
            civicEvent.outcome = dto.outcome;
            civicEvent.collaboration_developed = dto.collaboration_developed;
            civicEvent.engaged_CSOs_Or_NGOs = dto.engaged_CSOs_Or_NGOs;
            civicEvent.sponsoring_agency = dto.sponsoring_agency;
            civicEvent.grant_value = dto.grant_value;
            civicEvent.arranged_or_participated = dto.arranged_or_participated;
            civicEvent.dissemination_material = dto.dissemination_material;
            civicEvent.remarks = dto.remarks;
            civicEvent.evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : civicEvent.evidence;

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Civic Engagement Event updated successfully." });
        }
        [Authorize]
        [HttpPut("update-consultancy-contract/{id}")]
        public async Task<IActionResult> UpdateConsultancyContract(int id, ConsultancyContractDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            var consultancyContract = await _context.ConsultancyContracts.FindAsync(id);
            if (consultancyContract == null)
            {
                return NotFound("Consultancy Contract not found.");
            }

            consultancyContract.project_title = dto.project_title;
            consultancyContract.execution_date = dto.execution_date;
            consultancyContract.PI_name = dto.PI_name;
            consultancyContract.PI_designation = dto.PI_designation;
            consultancyContract.PI_department = dto.PI_department;
            consultancyContract.company_details = dto.company_details;
            consultancyContract.contract_value = dto.contract_value;
            consultancyContract.start_date = dto.start_date;
            consultancyContract.end_date = dto.end_date;
            consultancyContract.consultancy_type = dto.consultancy_type;
            consultancyContract.key_deliverables = dto.key_deliverables;
            consultancyContract.ORIC_percentage = dto.ORIC_percentage;
            consultancyContract.remarks = dto.remarks;
            consultancyContract.evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : consultancyContract.evidence;

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Consultancy Contract updated successfully." });
        }
        [Authorize]
        [HttpPut("update-liaison-asrb/{id}")]
        public async Task<IActionResult> UpdateLiaisonWithASRB(int id, LiaisonDevelopedDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            var liaison = await _context.LiaisonsASRB.FindAsync(id);
            if (liaison == null)
            {
                return NotFound("Liaison record not found.");
            }

            liaison.liaison_with = dto.liaison_with;
            liaison.execution_date = dto.execution_date;
            liaison.evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : liaison.evidence;

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Liaison with ASRB updated successfully." });
        }

        [Authorize]
        [HttpDelete("delete-ric-form/{id}")]
        public async Task<IActionResult> DeleteRicForm1(int id)
        {
            var ricForm = await _context.ric_form_1
                .Include(f => f.ResearchProjectSubmittedHEC)
                .Include(f => f.ResearchProjectSubmittedNonHEC)
                .Include(f => f.ResearchProjectApprovedHEC)
                .Include(f => f.ResearchProjectApprovedNonHEC)
                .Include(f => f.HECFundedResearchProjectCompleted)
                .Include(f => f.NonHECFundedResearchProjectCompleted)
                .Include(f => f.JointResearchProjectsSubmitted)
                .Include(f => f.JointResearchProjectsApproved)
                .Include(f => f.JointResearchProjectsCompleted)
                .Include(f => f.ContractResearchAwarded)
                .Include(f => f.PolicyAdvocacies)
                .Include(f => f.ResearchLinks)
                .Include(f => f.CivicEngagements)
                .Include(f => f.ConsultancyContracts)
                .Include(f => f.LiaisonsASRB)
                .FirstOrDefaultAsync(f => f.ric_form_1_id == id);

            if (ricForm == null)
            {
                return NotFound("RicForm1 record not found.");
            }

            _context.ric_form_1.Remove(ricForm);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "RicForm1 and its related sub-forms deleted successfully." });
        }
        [Authorize]
        [HttpDelete("delete-research-project-submitted-hec/{id}")]
        public async Task<IActionResult> DeleteResearchProjectSubmittedHEC(int id)
        {
            var entity = await _context.ResearchProjectSubmittedHEC.FindAsync(id);
            if (entity == null) return NotFound("Research Project Submitted (HEC) not found.");

            _context.ResearchProjectSubmittedHEC.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Research Project Submitted (HEC) deleted successfully." });
        }

        [Authorize]
        [HttpDelete("delete-research-project-submitted-nonhec/{id}")]
        public async Task<IActionResult> DeleteResearchProjectSubmittedNonHEC(int id)
        {
            var entity = await _context.ResearchProjectSubmittedNonHEC.FindAsync(id);
            if (entity == null) return NotFound("Research Project Submitted (Non-HEC) not found.");

            _context.ResearchProjectSubmittedNonHEC.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Research Project Submitted (Non-HEC) deleted successfully." });
        }

        [Authorize]
        [HttpDelete("delete-research-project-approved-hec/{id}")]
        public async Task<IActionResult> DeleteResearchProjectApprovedHEC(int id)
        {
            var entity = await _context.ResearchProjectApprovedHEC.FindAsync(id);
            if (entity == null) return NotFound("Research Project Approved (HEC) not found.");

            _context.ResearchProjectApprovedHEC.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Research Project Approved (HEC) deleted successfully." });
        }

        [Authorize]
        [HttpDelete("delete-research-project-approved-nonhec/{id}")]
        public async Task<IActionResult> DeleteResearchProjectApprovedNonHEC(int id)
        {
            var entity = await _context.ResearchProjectApprovedNonHEC.FindAsync(id);
            if (entity == null) return NotFound("Research Project Approved (Non-HEC) not found.");

            _context.ResearchProjectApprovedNonHEC.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Research Project Approved (Non-HEC) deleted successfully." });
        }

        [Authorize]
        [HttpDelete("delete-hec-funded-research-project-completed/{id}")]
        public async Task<IActionResult> DeleteHECFundedResearchProjectCompleted(int id)
        {
            var entity = await _context.HECFundedResearchProjectCompleted.FindAsync(id);
            if (entity == null) return NotFound("HEC Funded Research Project Completed not found.");

            _context.HECFundedResearchProjectCompleted.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "HEC Funded Research Project Completed deleted successfully." });
        }

        [Authorize]
        [HttpDelete("delete-non-hec-funded-research-project-completed/{id}")]
        public async Task<IActionResult> DeleteNonHECFundedResearchProjectCompleted(int id)
        {
            var entity = await _context.NonHECFundedResearchProjectCompleted.FindAsync(id);
            if (entity == null) return NotFound("Non-HEC Funded Research Project Completed not found.");

            _context.NonHECFundedResearchProjectCompleted.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Non-HEC Funded Research Project Completed deleted successfully." });
        }

        [Authorize]
        [HttpDelete("delete-joint-research-projects-submitted/{id}")]
        public async Task<IActionResult> DeleteJointResearchProjectsSubmitted(int id)
        {
            var entity = await _context.JointResearchProjectsSubmitted.FindAsync(id);
            if (entity == null) return NotFound("Joint Research Project Submitted not found.");

            _context.JointResearchProjectsSubmitted.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Joint Research Project Submitted deleted successfully." });
        }

        [Authorize]
        [HttpDelete("delete-joint-research-projects-approved/{id}")]
        public async Task<IActionResult> DeleteJointResearchProjectsApproved(int id)
        {
            var entity = await _context.JointResearchProjectsApproved.FindAsync(id);
            if (entity == null) return NotFound("Joint Research Project Approved not found.");

            _context.JointResearchProjectsApproved.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Joint Research Project Approved deleted successfully." });
        }

        [Authorize]
        [HttpDelete("delete-joint-research-projects-completed/{id}")]
        public async Task<IActionResult> DeleteJointResearchProjectsCompleted(int id)
        {
            var entity = await _context.JointResearchProjectsCompleted.FindAsync(id);
            if (entity == null) return NotFound("Joint Research Project Completed not found.");

            _context.JointResearchProjectsCompleted.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Joint Research Project Completed deleted successfully." });
        }

        [Authorize]
        [HttpDelete("delete-contract-research-awarded/{id}")]
        public async Task<IActionResult> DeleteContractResearchAwarded(int id)
        {
            var entity = await _context.ContractResearchAwarded.FindAsync(id);
            if (entity == null) return NotFound("Contract Research Awarded record not found.");

            _context.ContractResearchAwarded.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Contract Research Awarded deleted successfully." });
        }

        [Authorize]
        [HttpDelete("delete-policy-advocacy/{id}")]
        public async Task<IActionResult> DeletePolicyAdvocacy(int id)
        {
            var entity = await _context.PolicyAdvocacies.FindAsync(id);
            if (entity == null) return NotFound("Policy Advocacy Case Study not found.");

            _context.PolicyAdvocacies.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Policy Advocacy Case Study deleted successfully." });
        }

        [Authorize]
        [HttpDelete("delete-research-links/{id}")]
        public async Task<IActionResult> DeleteResearchLinks(int id)
        {
            var entity = await _context.ResearchLinks.FindAsync(id);
            if (entity == null) return NotFound("Research Link not found.");

            _context.ResearchLinks.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Research Link deleted successfully." });
        }

        [Authorize]
        [HttpDelete("delete-civic-engagement/{id}")]
        public async Task<IActionResult> DeleteCivicEngagement(int id)
        {
            var entity = await _context.CivicEngagements.FindAsync(id);
            if (entity == null) return NotFound("Civic Engagement Event not found.");

            _context.CivicEngagements.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Civic Engagement Event deleted successfully." });
        }

        [Authorize]
        [HttpDelete("delete-consultancy-contract/{id}")]
        public async Task<IActionResult> DeleteConsultancyContract(int id)
        {
            var entity = await _context.ConsultancyContracts.FindAsync(id);
            if (entity == null) return NotFound("Consultancy Contract not found.");

            _context.ConsultancyContracts.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Consultancy Contract deleted successfully." });
        }

        [Authorize]
        [HttpDelete("delete-liaison-asrb/{id}")]
        public async Task<IActionResult> DeleteLiaisonASRB(int id)
        {
            var entity = await _context.LiaisonsASRB.FindAsync(id);
            if (entity == null) return NotFound("Liaison Developed With ASRB not found.");

            _context.LiaisonsASRB.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Liaison Developed With ASRB deleted successfully." });
        }



    }
}
