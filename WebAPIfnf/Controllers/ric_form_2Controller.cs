using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApi.Data;
using WebApi.Models;
using WebApi.Dtos;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;



namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RicForm2Controller : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RicForm2Controller> _logger;
        private readonly IConfiguration _configuration;

        public RicForm2Controller(ApplicationDbContext context, IConfiguration configuration, ILogger<RicForm2Controller> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        // Submit ric_form_2 data
        [Authorize]
        [HttpPost("submit")]
        public async Task<IActionResult> SubmitRicForm2(ric_form_2Dto formDto)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("Invalid user ID in token.");
            }

            var ricForm2 = new ric_form_2
            {
                dataoric_id = userId,
                faculty_name = formDto.faculty_name,
                department_name = formDto.department_name, 
                faculty_email = formDto.faculty_email,
                ip_disclosures_made = formDto.ip_disclosures_made,
                patents_filed = formDto.patents_filed,
                patents_granted = formDto.patents_granted,
                ip_licensing_negotiations_initiated= formDto.ip_licensing_negotiations_initiated,
                licenses_signed = formDto.licenses_signed,
                products_prototypes_developed = formDto.products_prototypes_developed,
                products_prototypes_displayed = formDto.products_prototypes_displayed,
                industry_visits = formDto.industry_visits,
                agreements_signed = formDto.agreements_signed,
                honors_awards_won = formDto.honors_awards_won,
                oric_trainings_arranged = formDto.oric_trainings_arranged,
                external_trainings_arranged = formDto.external_trainings_arranged,
                research_publications = formDto.research_publications
            };

            try
            {
                _context.ric_form_2.Add(ricForm2);
                await _context.SaveChangesAsync();
                return Ok("RIC Form 2 data submitted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error saving data: {ex.Message}");
                if (ex.InnerException != null)
                {
                    _logger.LogError($"Inner Exception: {ex.InnerException.Message}");
                    _logger.LogError($"Inner Exception Stack Trace: {ex.InnerException.StackTrace}");
                }
                return StatusCode(500, $"Error saving data: {ex.Message}");
            }
        }

        // API to Get Filtered ric_form_2 Data
        [Authorize]
        [HttpPost("get")]
        public async Task<IActionResult> GetRicForm2Data([FromBody] Dictionary<string, string> filters)
        {
            var query = _context.ric_form_2.AsQueryable();

            if (filters.TryGetValue("faculty_name", out string? facultyName) && !string.IsNullOrEmpty(facultyName))
            {
                query = query.Where(f => f.faculty_name.Contains(facultyName));
            }

            if (filters.TryGetValue("department_name", out string? departmentName) && !string.IsNullOrEmpty(departmentName))
            {
                query = query.Where(f => f.department_name.Contains(departmentName));
            }

            if (filters.TryGetValue("email", out string? email) && !string.IsNullOrEmpty(email))
            {
                query = query.Where(f => f.faculty_email == email);
            }

            try
            {
                var result = await query.ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving data: {ex.Message}");
            }
        }
    }
}
