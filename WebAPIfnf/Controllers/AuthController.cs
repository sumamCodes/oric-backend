using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebApi.Dtos;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext context, IConfiguration configuration, ILogger<AuthController> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        //[HttpGet("health")]
        //public IActionResult HealthCheck()
        //{
        //    return Ok(new { status = "API is running" });
        //}

        [HttpGet("health")]
        public IActionResult HealthCheck()
        {
            _logger.LogInformation("HealthCheck endpoint was called.");
            return Ok(new { status = "API is running" });
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            Console.WriteLine("Login attempt with email: " + loginDto.email);

            var user = await _context.dataoric.SingleOrDefaultAsync(u => u.email == loginDto.email);

            if (user == null || user.password != loginDto.password)
            {
                Console.WriteLine("Invalid credentials for email: " + loginDto.email);
                return Unauthorized("Invalid credentials");
            }

            Console.WriteLine("Login successful for user: " + user.name);
            var token = GenerateJwtToken(user);

            // Print token details to the con'sole
            Console.WriteLine("JWT Token: " + token);
            Console.WriteLine("User email: " + user.email);
            Console.WriteLine("User role: " + user.role);
            Console.WriteLine("User name: " + user.name);

            // Return the token and user details in the respons
            return Ok(new
            {
                Token = token,
                Email = user.email,
                Role = user.role,
                Name = user.name
            });
        }

        private string GenerateJwtToken(Login user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key not set"));

            var keyString = _configuration["Jwt:Key"]; // Read the key as a string

            if (string.IsNullOrEmpty(keyString))
            {
                throw new InvalidOperationException("JWT key is missing. Ensure environment variable is loaded.");
            }

            var keyBytes = Encoding.UTF8.GetBytes(keyString); // Convert it to bytes
            var securityKey = new SymmetricSecurityKey(keyBytes);

            Console.WriteLine($"Using JWT Key: {key}");

            // ✅ Use List<Claim> instead of Claim[]
            var claims = new List<Claim>
{
                new Claim(ClaimTypes.Name, user.name),
                new Claim(ClaimTypes.Email, user.email),
                new Claim(ClaimTypes.Role, user.role),
                new Claim("UserId", user.dataoric_id.ToString()),
                 new Claim("department_id", user.department_id?.ToString() ?? "0") // dataoric_id should always exist
};

            // Ensure department_id is not null before adding it
            if (user.department_id != null)
            {
                claims.Add(new Claim("department_id", user.department_id?.ToString() ?? "0"));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),  // ✅ No need to convert to an array
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        //[Authorize]
        //[HttpPost("submit-ric-form")]
        //public async Task<IActionResult> SubmitRicForm(ric_form_1Dto formDto)
        //{
        //    // Log incoming data
        //    _logger.LogInformation($"Received data: {JsonConvert.SerializeObject(formDto)}");

        //    var userIdClaim = User.FindFirst("UserId")?.Value;
        //    if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
        //    {
        //        _logger.LogWarning("Invalid user ID in token.");
        //        return Unauthorized("Invalid user ID in token.");
        //    }


        // Create the RicForm1 entity
        //            var ricForm = new ric_form_1
        //            {
        //                dataoric_id = userId,
        //                faculty_name = formDto.faculty_name,
        //                department_name = formDto.department_name,
        //                faculty_email = formDto.faculty_email,
        //                research_grants_submitted_hec = formDto.research_grants_submitted_hec,
        //                research_grants_submitted_non_hec = formDto.research_grants_submitted_non_hec,
        //                research_grants_approved_hec = formDto.research_grants_approved_hec,
        //                research_grants_approved_non_hec = formDto.research_grants_approved_non_hec,
        //                hec_funded_projects_completed = formDto.hec_funded_projects_completed,
        //                non_hec_funded_projects_completed = formDto.non_hec_funded_projects_completed,
        //                joint_projects_submitted = formDto.joint_projects_submitted,
        //                joint_projects_approved = formDto.joint_projects_approved,
        //                joint_projects_completed = formDto.joint_projects_completed,
        //                policy_advocacy_case_studies = formDto.policy_advocacy_case_studies,
        //                research_links_established = formDto.research_links_established,
        //                civic_engagements = formDto.civic_engagements,
        //                consultancy_contracts_executed = formDto.consultancy_contracts_executed,
        //                liaison_with_asrb = formDto.liaison_with_asrb,

        //                // New mappings
        //                research_project_submitted_type = formDto.research_project_submitted_type,
        //                submitted_grant_name = formDto.submitted_grant_name,
        //                submitted_proposal_submission_date = formDto.submitted_proposal_submission_date,
        //                submitted_pi_name = formDto.submitted_pi_name,
        //                submitted_pi_designation = formDto.submitted_pi_designation,
        //                submitted_pi_department = formDto.submitted_pi_department,
        //                submitted_thematic_area = formDto.submitted_thematic_area,
        //                submitted_research_title = formDto.submitted_research_title,
        //                submitted_duration_start = formDto.submitted_duration_start,
        //                submitted_duration_end = formDto.submitted_duration_end,
        //                submitted_total_funding_requested = formDto.submitted_total_funding_requested,
        //                submitted_collaborating_partners = formDto.submitted_collaborating_partners,
        //                submitted_co_funding_partners = formDto.submitted_co_funding_partners,
        //                submitted_national_or_international = formDto.submitted_national_or_international,
        //                submitted_status = formDto.submitted_status,
        //                submitted_remarks = formDto.submitted_remarks,
        //                submitted_evidence = formDto.submitted_evidence,
        //                approved_grant_name=formDto.approved_grant_name,
        //                approved_proposal_approval_date=formDto.approved_proposal_approval_date,
        //                approved_pi_name = formDto.approved_pi_name,
        //                approved_pi_designation = formDto.approved_pi_designation,
        //                approved_pi_department = formDto.approved_pi_department,
        //                approved_thematic_area = formDto.approved_thematic_area,
        //                approved_research_title = formDto.approved_research_title,
        //                approved_duration_start = formDto.approved_duration_start,
        //                approved_duration_end = formDto.approved_duration_end,
        //                approved_total_funding_approved = formDto.approved_total_funding_approved,
        //                approved_collaborating_partners = formDto.approved_collaborating_partners,
        //                approved_co_funding_partners = formDto.approved_co_funding_partners,
        //                approved_national_or_international = formDto.approved_national_or_international,
        //                approved_status = formDto.approved_status,
        //                approved_remarks = formDto.approved_remarks,
        //                approved_evidence = formDto.approved_evidence,
        //                completed_grant_name = formDto.completed_grant_name,
        //                completed_project_completion_date = formDto.completed_project_completion_date,
        //                completed_pi_name = formDto.completed_pi_name,
        //                completed_pi_designation = formDto.completed_pi_designation,
        //                completed_pi_department = formDto.completed_pi_department ,
        //                completed_thematic_area = formDto.completed_thematic_area,
        //                completed_research_title = formDto.completed_research_title,
        //                completed_duration_start = formDto.completed_duration_start,
        //                completed_duration_end = formDto.completed_duration_end,
        //                completed_total_funding_utilized = formDto.completed_total_funding_utilized,
        //                completed_total_funding_released =formDto.completed_total_funding_released,
        //                completed_status = formDto.completed_status,
        //                completed_key_deliverables = formDto.completed_key_deliverables,
        //                completed_evidence=formDto.completed_evidence,
        //                joint_research_type = formDto.joint_research_type,
        //                joint_grant_name=formDto.joint_grant_name,
        //                joint_funding_agency = formDto.joint_funding_agency,
        //                joint_project_submission_date = formDto.joint_project_submission_date,
        //                joint_project_approval_date = formDto.joint_project_approval_date,
        //                joint_project_completion_date=formDto.joint_project_completion_date,
        //                joint_pi_name=formDto.joint_pi_name,
        //                joint_pi_designation = formDto.joint_pi_designation,
        //                joint_pi_department=formDto.joint_pi_department,
        //                joint_co_pi_name = formDto.joint_co_pi_name,
        //                joint_co_pi_designation = formDto.joint_co_pi_designation,
        //                joint_co_pi_department=formDto.joint_co_pi_department,
        //                joint_co_pi_university = formDto.joint_co_pi_university,
        //                joint_thematic_area=formDto.joint_thematic_area,
        //                joint_research_title = formDto.joint_research_title,
        //                joint_duration_start=formDto.joint_duration_start,
        //                joint_duration_end = formDto.joint_duration_end,
        //                joint_total_funding = formDto.joint_total_funding,
        //                joint_co_funding_partners=formDto.joint_co_funding_partners,
        //                joint_status = formDto.joint_status,
        //                joint_remarks=formDto.joint_remarks,
        //                joint_evidence = formDto.joint_evidence,
        //                contract_thematic_area=formDto.contract_thematic_area,
        //                contract_research_title = formDto.contract_research_title,
        //                contract_signed_date = formDto.contract_signed_date,
        //                contract_pi_name=formDto.contract_pi_name,
        //                contract_pi_designation = formDto.contract_pi_designation,
        //                contract_pi_department=formDto.contract_pi_department,
        //                contract_co_pi_name = formDto.contract_co_pi_name,
        //                contract_co_pi_designation=formDto.contract_co_pi_designation,
        //                contract_co_pi_department = formDto.contract_co_pi_department,
        //                contract_sponsoring_agency = formDto.contract_sponsoring_agency,
        //                contract_sponsoring_address=formDto.contract_sponsoring_address,
        //                contract_national_or_international = formDto.contract_national_or_international,
        //                contract_counterpart_industry=formDto.contract_counterpart_industry,
        //                contract_duration_start = formDto.contract_duration_start,
        //                contract_duration_end=formDto.contract_duration_end,
        //                contract_total_amount = formDto.contract_total_amount,
        //                contract_expected_deliverables = formDto.contract_expected_deliverables,
        //                contract_date=formDto.contract_date,
        //                contract_evidence = formDto.contract_evidence,
        //                policy_advocacy_body=formDto.policy_advocacy_body,
        //                policy_advocacy_date=formDto.policy_advocacy_date,
        //                policy_advocacy_pi_name = formDto.policy_advocacy_pi_name,
        //                policy_advocacy_area = formDto.policy_advocacy_area,
        //                policy_advocacy_brief = formDto.policy_advocacy_brief,
        //                policy_advocacy_duration_start = formDto.policy_advocacy_duration_start,
        //                policy_advocacy_duration_end = formDto.policy_advocacy_duration_end,
        //                policy_advocacy_coalition_partners = formDto.policy_advocacy_coalition_partners,
        //                policy_advocacy_research_status = formDto.policy_advocacy_research_status,
        //                policy_advocacy_tools = formDto.policy_advocacy_tools,
        //                policy_advocacy_evidence = formDto.policy_advocacy_evidence,
        //                research_links_type=formDto.research_links_type,
        //                research_links_mou_date = formDto.research_links_mou_date,
        //                research_links_national_or_international = formDto.research_links_national_or_international,
        //                research_links_host_institution = formDto.research_links_host_institution,
        //                research_links_collaborating_agency = formDto.research_links_collaborating_agency,
        //                research_links_field_of_study = formDto.research_links_field_of_study,
        //                research_links_scope = formDto.research_links_scope,
        //                research_links_salient_features = formDto.research_links_salient_features,
        //                research_links_evidence = formDto.research_links_evidence,
        //                civic_event_title = formDto.civic_event_title,
        //                civic_event_date = formDto.civic_event_date,
        //                civic_event_component = formDto.civic_event_component,
        //                civic_event_outcome = formDto.civic_event_outcome,
        //                civic_event_collaboration=formDto.civic_event_collaboration,
        //                civic_event_csos = formDto.civic_event_csos,
        //                civic_event_sponsoring_agency = formDto.civic_event_sponsoring_agency,
        //                civic_event_grant_value = formDto.civic_event_grant_value,
        //                civic_event_participation = formDto.civic_event_participation,
        //                civic_event_dissemination_material = formDto.civic_event_dissemination_material,
        //                civic_event_remarks = formDto.civic_event_remarks,
        //                civic_event_evidence = formDto.civic_event_evidence,
        //                consultancy_title=formDto.consultancy_title,
        //                consultancy_execution_date = formDto.consultancy_execution_date,
        //                consultancy_pi_name = formDto.consultancy_pi_name,
        //                consultancy_pi_designation = formDto.consultancy_pi_designation,
        //                consultancy_pi_department = formDto.consultancy_pi_department,
        //                consultancy_company_details = formDto.consultancy_company_details,
        //                consultancy_contract_value = formDto.consultancy_contract_value,
        //                consultancy_project_timelines_start = formDto.consultancy_project_timelines_start,
        //                consultancy_project_timelines_end = formDto.consultancy_project_timelines_end,
        //                consultancy_type_of_services = formDto.consultancy_type_of_services,
        //                consultancy_key_deliverables = formDto.consultancy_key_deliverables,
        //                consultancy_oric_percentage = formDto.consultancy_oric_percentage,
        //                consultancy_remarks = formDto.consultancy_remarks,
        //                consultancy_evidence = formDto.consultancy_evidence,
        //                liaison_developed_with = formDto.liaison_developed_with,
        //                liaison_execution_date = formDto.liaison_execution_date,
        //                liaison_evidence = formDto.liaison_evidence

        //            };

        //            try
        //            {
        //                _context.ric_form_1.Add(ricForm);
        //                await _context.SaveChangesAsync();
        //                _logger.LogInformation("RIC Form 1 data submitted successfully.");
        //                return Ok("RIC Form 1 data submitted successfully.");
        //            }
        //            catch (DbUpdateException dbEx)
        //            {
        //                _logger.LogError($"Database update error occurred: {dbEx.Message}");
        //                return StatusCode(500, "An error occurred while saving the data to the database.");
        //            }
        //            catch (Exception ex)
        //            {
        //                _logger.LogError($"Error occurred while saving RIC form data: {ex.Message}");
        //                if (ex.InnerException != null)
        //                {
        //                    _logger.LogError($"Inner Exception: {ex.InnerException.Message}");
        //                    _logger.LogError($"Inner Exception Stack Trace: {ex.InnerException.StackTrace}");
        //                }
        //                return StatusCode(500, "An error occurred while saving the data.");
        //            }
        //        }


        //        [Authorize] // Optional: Add this if you want to restrict access to authenticated users.
        //        [HttpGet("get-all-ric-forms")]
        //        public async Task<IActionResult> GetAllRicForms()
        //        {
        //            try
        //            {
        //                // Retrieve all data from ric_form_1 table
        //                var ricForms = await _context.ric_form_1
        //                    .Select(ricForm => new ric_form_1Dto
        //                    {
        //                        faculty_name = ricForm.faculty_name,
        //                        department_name = ricForm.department_name,
        //                        faculty_email = ricForm.faculty_email,
        //                        research_grants_submitted_hec = ricForm.research_grants_submitted_hec,
        //                        research_grants_submitted_non_hec = ricForm.research_grants_submitted_non_hec,
        //                        research_grants_approved_hec = ricForm.research_grants_approved_hec,
        //                        research_grants_approved_non_hec = ricForm.research_grants_approved_non_hec,
        //                        hec_funded_projects_completed = ricForm.hec_funded_projects_completed,
        //                        non_hec_funded_projects_completed = ricForm.non_hec_funded_projects_completed,
        //                        joint_projects_submitted = ricForm.joint_projects_submitted,
        //                        joint_projects_approved = ricForm.joint_projects_approved,
        //                        joint_projects_completed = ricForm.joint_projects_completed,
        //                        policy_advocacy_case_studies = ricForm.policy_advocacy_case_studies,
        //                        research_links_established = ricForm.research_links_established,
        //                        civic_engagements = ricForm.civic_engagements,
        //                        consultancy_contracts_executed = ricForm.consultancy_contracts_executed,
        //                        liaison_with_asrb = ricForm.liaison_with_asrb,


        //                    })
        //                    .ToListAsync();

        //                if (ricForms == null || ricForms.Count == 0)
        //                {
        //                    return NotFound("No RIC Form data found.");
        //                }

        //                return Ok(ricForms); // Return the data as JSON
        //            }
        //            catch (Exception ex)
        //            {
        //                _logger.LogError($"Error occurred while retrieving RIC Form data: {ex.Message}");
        //                return StatusCode(500, "An error occurred while retrieving the data.");
        //            }
        //        }
        //        [Authorize] // Optional: Add this if you want to restrict access.
        //        [HttpPost("filter-ric-forms")]
        //        public async Task<IActionResult> FilterRicForms([FromBody] RicFormFilterDto filter)
        //        {
        //            try
        //            {
        //                // Start with the base query
        //                var query = _context.ric_form_1.AsQueryable();

        //                // Apply filters dynamically
        //                if (!string.IsNullOrEmpty(filter.faculty_name))
        //                    query = query.Where(r => r.faculty_name.Contains(filter.faculty_name));

        //                if (!string.IsNullOrEmpty(filter.department_name))
        //                    query = query.Where(r => r.department_name.Contains(filter.department_name));

        //                if (!string.IsNullOrEmpty(filter.faculty_email))
        //                    query = query.Where(r => r.faculty_email.Contains(filter.faculty_email));

        //                if (filter.research_grants_submitted_hec.HasValue)
        //                    query = query.Where(r => r.research_grants_submitted_hec == filter.research_grants_submitted_hec);

        //                if (filter.research_grants_approved_non_hec.HasValue)
        //                    query = query.Where(r => r.research_grants_approved_non_hec == filter.research_grants_approved_non_hec);

        //                // Execute the query and project the results into DTO
        //                var results = await query.Select(r => new ric_form_1Dto
        //                {
        //                    faculty_name = r.faculty_name,
        //                    department_name = r.department_name,
        //                    faculty_email = r.faculty_email,
        //                    research_grants_submitted_hec = r.research_grants_submitted_hec,
        //                    research_grants_submitted_non_hec = r.research_grants_submitted_non_hec,
        //                    research_grants_approved_hec = r.research_grants_approved_hec,
        //                    research_grants_approved_non_hec = r.research_grants_approved_non_hec,
        //                    hec_funded_projects_completed = r.hec_funded_projects_completed,
        //                    non_hec_funded_projects_completed = r.non_hec_funded_projects_completed,
        //                    joint_projects_submitted = r.joint_projects_submitted,
        //                    joint_projects_approved = r.joint_projects_approved,
        //                    joint_projects_completed = r.joint_projects_completed,
        //                    policy_advocacy_case_studies = r.policy_advocacy_case_studies,
        //                    research_links_established = r.research_links_established,
        //                    civic_engagements = r.civic_engagements,
        //                    consultancy_contracts_executed = r.consultancy_contracts_executed,
        //                    liaison_with_asrb = r.liaison_with_asrb
        //                }).ToListAsync();

        //                if (!results.Any())
        //                {
        //                    return NotFound("No matching records found.");
        //                }

        //                return Ok(results);
        //            }
        //            catch (Exception ex)
        //            {
        //                _logger.LogError($"Error occurred while filtering RIC forms: {ex.Message}");
        //                return StatusCode(500, "An error occurred while retrieving the data.");
        //            }
        //        }
    }
}

