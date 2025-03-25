using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApi.Data;
using WebApi.Models;
using WebApi.Dtos;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RicForm3Controller : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RicForm3Controller> _logger;

        public RicForm3Controller(ApplicationDbContext context, ILogger<RicForm3Controller> logger)
        {
            _context = context;
            _logger = logger;
        }

        [Authorize]
        [HttpPost("submit")]
        public async Task<IActionResult> SubmitRicForm3(RicForm3Dto dto)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("Invalid user ID in token.");
            }

            var ricForm = new RicForm3
            {
                dataoric_id = userId,
                full_name = dto.full_name,
                designation = dto.designation,
                department = dto.department,
                email = dto.email,
                number_faculty_led_startups = dto.number_faculty_led_startups,
                number_spin_offs = dto.number_spin_offs,
                jobs_created_retained = dto.jobs_created_retained,
                students_placed = dto.students_placed,
                participation_count = dto.participation_count
            };

            _context.ric_form_3.Add(ricForm);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "RIC Form 3 submitted successfully.", RicForm3Id = ricForm.ric_form_3_id });
        }

        [HttpPost("faculty_startups")]
        public async Task<IActionResult> SubmitFacultyStartup(FacultyStartupsDto dto)
        {
            // Ensure the related RicForm3 exists
            var ricForm = await _context.ric_form_3.FindAsync(dto.ric_form_3_id);
            if (ricForm == null)
            {
                return NotFound("RicForm3 record not found.");
            }
            var startup = new FacultyStartups
            {
                RicForm3 = ricForm,
                startup_name = dto.startup_name,
                sector = dto.sector,
                stage = dto.stage,
                ip_status = dto.ip_status,
                license_agreement = dto.license_agreement,
                funding_source = dto.funding_source,
                revenue = dto.revenue,
                internships_created = dto.internships_created,
                jobs_created = dto.jobs_created,
                evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : null
            };

            _context.FacultyStartups.Add(startup);
            await _context.SaveChangesAsync();

            return Ok("Faculty startup submitted successfully.");
        }

        [HttpPost("spin_offs")]
        public async Task<IActionResult> AddSpinOffs(SpinOffsDto dto)
        {
            var ricForm = await _context.ric_form_3.FindAsync(dto.ric_form_3_id);
            if (ricForm == null)
            {
                return NotFound("RicForm3 record not found.");
            }
            var spinoff = new SpinOffs
            {
                RicForm3 = ricForm,
                spinoff_name = dto.spinoff_name,
                stage = dto.stage,
                license_agreement = dto.license_agreement,
                revenue = dto.revenue,
                evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : null
            };

            _context.SpinOffs.Add(spinoff);
            await _context.SaveChangesAsync();

            return Ok("Spin-off submitted successfully.");
        }

        [HttpPost("funding")]
        public async Task<IActionResult> AddFunding(FundingDto dto)
        {
            var ricForm = await _context.ric_form_3.FindAsync(dto.ric_form_3_id);
            if (ricForm == null)
            {
                return NotFound("RicForm3 record not found.");
            }
            var funding = new Funding
            {
                RicForm3 = ricForm,
                startup_details = dto.startup_details,
                funding_agency = dto.funding_agency,
                funding_type = dto.funding_type,
                amount = dto.amount,
                agreement_signed = dto.agreement_signed,
                in_kind_support = dto.in_kind_support,
                evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : null
            };

            _context.Funding.Add(funding);
            await _context.SaveChangesAsync();

            return Ok("Funding submitted successfully.");
        }

        [HttpPost("events")]
        public async Task<IActionResult> AddEvents(EventsDto dto)
        {
            var ricForm = await _context.ric_form_3.FindAsync(dto.ric_form_3_id);
            if (ricForm == null)
            {
                return NotFound("RicForm3 record not found.");
            }
            var eventRecord = new Events
            {
                RicForm3 = ricForm,
                title = dto.title,
                event_date = dto.event_date,
                venue = dto.venue,
                field = dto.field,
                panelist_details = dto.panelist_details,
                organizers = dto.organizers,
                audience = dto.audience,
                participants_count = dto.participants_count,
                evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : null
            };

            _context.Events.Add(eventRecord);
            await _context.SaveChangesAsync();

            return Ok("Event submitted successfully.");
        }


        //[HttpGet("GetRicForm3")]
        //[Authorize]
        //public async Task<IActionResult> GetRicForm3()
        //{
        //    // Extract user claims
        //    var userIdClaim = User.FindFirst("UserId")?.Value;
        //    var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
        //    var departmentIdClaim = User.FindFirst("department_id")?.Value; // May be null for admin

        //    // Debugging: Print all claims
        //    Console.WriteLine("---- User Claims ----");
        //    foreach (var claim in User.Claims)
        //    {
        //        Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
        //    }

        //    // Validate essential claims
        //    if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(roleClaim))
        //    {
        //        return Unauthorized("Invalid token, missing user information.");
        //    }

        //    int userId = int.Parse(userIdClaim);  // ✅ Guaranteed to exist
        //    string role = roleClaim;
        //    int? departmentId = null;

        //    // Parse department_id only if it exists
        //    if (!string.IsNullOrEmpty(departmentIdClaim))
        //    {
        //        departmentId = int.Parse(departmentIdClaim);  // ✅ Avoids null parsing error
        //    }

        //    // Build base query with subforms included
        //    IQueryable<RicForm3> query = _context.ric_form_3
        //        .Include(r => r.FacultyStartups)
        //        .Include(r => r.SpinOffs)
        //        .Include(r => r.Funding)
        //        .Include(r => r.Events);

        //    // Role-based filtering
        //    if (role == "admin")
        //    {
        //        return Ok(await query.ToListAsync());  // ✅ Admin gets all records
        //    }
        //    else if (role == "dean" && departmentId.HasValue)
        //    {
        //        // Dean gets records where `dataoric_id` belongs to their department
        //        var departmentUsers = await _context.dataoric
        //            .Where(d => d.department_id == departmentId)
        //            .Select(d => d.dataoric_id)
        //            .ToListAsync();

        //        return Ok(await query.Where(r => departmentUsers.Contains(r.dataoric_id)).ToListAsync());
        //    }
        //    else if (role == "faculty")
        //    {
        //        return Ok(await query.Where(r => r.dataoric_id == userId).ToListAsync());  // ✅ Faculty gets own records
        //    }
        //    else
        //    {
        //        return Forbid();
        //    }
        //    var forms = await query.ToListAsync();

        //    // ✅ Convert evidence fields from byte[] to Base64
        //    var result = forms.Select(form => new
        //    {
        //        form.ric_form_3_id,
        //        form.dataoric_id,
        //        FacultyStartups = form.FacultyStartups?.Select(fs => new { fs.funding_id, Evidence = fs.evidence != null ? FileHelper.ConvertByteArrayToBase64(fs.evidence) : null }),
        //        SpinOffs = form.SpinOffs?.Select(so => new { so.id, Evidence = so.evidence != null ? FileHelper.ConvertByteArrayToBase64(so.evidence) : null }),
        //        Funding = form.Funding?.Select(f => new { f.id, Evidence = f.evidence != null ? FileHelper.ConvertByteArrayToBase64(f.evidence) : null }),
        //        Events = form.Events?.Select(e => new { e.id, Evidence = e.evidence != null ? FileHelper.ConvertByteArrayToBase64(e.evidence) : null }),
        //    });

        //    return Ok(result);
        //}

        [HttpGet("GetRicForm3")]
        [Authorize]
        public async Task<IActionResult> GetRicForm3()
        {
            // Extract user claims
            var userIdClaim = User.FindFirst("UserId")?.Value;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            var departmentIdClaim = User.FindFirst("department_id")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(roleClaim))
                return Unauthorized("Invalid token, missing user information.");

            int userId = int.Parse(userIdClaim);
            string role = roleClaim;
            int? departmentId = !string.IsNullOrEmpty(departmentIdClaim) ? int.Parse(departmentIdClaim) : null;

            // Query with subforms
            IQueryable<RicForm3> query = _context.ric_form_3
                .Include(r => r.FacultyStartups)
                .Include(r => r.SpinOffs)
                .Include(r => r.Funding)
                .Include(r => r.Events);

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

            // ✅ Convert evidence fields from byte[] to Base64
            var result = forms.Select(form => new
            {
                form.ric_form_3_id,
                form.dataoric_id,
                FacultyStartups = form.FacultyStartups?.Select(fs => new { fs.startup_id, Evidence = fs.evidence != null ? FileHelper.ConvertByteArrayToBase64(fs.evidence) : null }),
                SpinOffs = form.SpinOffs?.Select(so => new { so.spinoff_id, Evidence = so.evidence != null ? FileHelper.ConvertByteArrayToBase64(so.evidence) : null }),
                Funding = form.Funding?.Select(f => new { f.funding_id, Evidence = f.evidence != null ? FileHelper.ConvertByteArrayToBase64(f.evidence) : null }),
                Events = form.Events?.Select(e => new { e.event_id, Evidence = e.evidence != null ? FileHelper.ConvertByteArrayToBase64(e.evidence) : null }),
            });

            return Ok(result);
        }
        [HttpPut("updateRicForm3/{id}")]
        public async Task<IActionResult> UpdateRicForm3(int id, RicForm3Dto dto)
        {
            var ricForm = await _context.ric_form_3.FindAsync(id);
            if (ricForm == null)
            {
                return NotFound("RicForm3 record not found.");
            }

            // Only update fields if they are different from the existing values
            if (!string.IsNullOrEmpty(dto.full_name)) ricForm.full_name = dto.full_name;
            if (!string.IsNullOrEmpty(dto.designation)) ricForm.designation = dto.designation;
            if (!string.IsNullOrEmpty(dto.department)) ricForm.department = dto.department;
            if (!string.IsNullOrEmpty(dto.email)) ricForm.email = dto.email;

            // No null check needed, just update the integer fields
            if (dto.number_faculty_led_startups != ricForm.number_faculty_led_startups)
                ricForm.number_faculty_led_startups = dto.number_faculty_led_startups;

            if (dto.number_spin_offs != ricForm.number_spin_offs)
                ricForm.number_spin_offs = dto.number_spin_offs;

            if (dto.jobs_created_retained != ricForm.jobs_created_retained)
                ricForm.jobs_created_retained = dto.jobs_created_retained;

            if (dto.students_placed != ricForm.students_placed)
                ricForm.students_placed = dto.students_placed;

            if (dto.participation_count != ricForm.participation_count)
                ricForm.participation_count = dto.participation_count;

            await _context.SaveChangesAsync();
            return Ok("RicForm3 updated successfully.");
        }


        [HttpPut("updateFacultyStartup/{id}")]
        public async Task<IActionResult> UpdateFacultyStartup(int id, FacultyStartupsDto dto)
        {
            var startup = await _context.FacultyStartups.FindAsync(id);
            if (startup == null)
            {
                return NotFound("Faculty Startup record not found.");
            }

            startup.startup_name = dto.startup_name ?? startup.startup_name;
            startup.sector = dto.sector ?? startup.sector;
            startup.stage = dto.stage ?? startup.stage;
            startup.ip_status = dto.ip_status ?? startup.ip_status;
            startup.license_agreement = dto.license_agreement ?? startup.license_agreement;
            startup.funding_source = dto.funding_source ?? startup.funding_source;
            startup.revenue = dto.revenue ?? startup.revenue;
            startup.internships_created = dto.internships_created ?? startup.internships_created;
            startup.jobs_created = dto.jobs_created ?? startup.jobs_created;
            startup.evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : startup.evidence;

            await _context.SaveChangesAsync();
            return Ok("Faculty Startup updated successfully.");
        }

        // Similar update APIs for SpinOffs, Funding, and Events

        [HttpPut("updateSpinOffs/{id}")]
        public async Task<IActionResult> UpdateSpinOffs(int id, SpinOffsDto dto)
        {
            var spinoff = await _context.SpinOffs.FindAsync(id);
            if (spinoff == null)
            {
                return NotFound("SpinOff record not found.");
            }

            spinoff.spinoff_name = dto.spinoff_name ?? spinoff.spinoff_name;
            spinoff.stage = dto.stage ?? spinoff.stage;
            spinoff.license_agreement = dto.license_agreement ?? spinoff.license_agreement;
            spinoff.revenue = dto.revenue ?? spinoff.revenue;
            spinoff.evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : spinoff.evidence;

            await _context.SaveChangesAsync();
            return Ok("SpinOff updated successfully.");
        }

        [HttpPut("updateFunding/{id}")]
        public async Task<IActionResult> UpdateFunding(int id, FundingDto dto)
        {
            var funding = await _context.Funding.FindAsync(id);
            if (funding == null)
            {
                return NotFound("Funding record not found.");
            }

            funding.startup_details = dto.startup_details ?? funding.startup_details;
            funding.funding_agency = dto.funding_agency ?? funding.funding_agency;
            funding.funding_type = dto.funding_type ?? funding.funding_type;
            funding.amount = dto.amount ?? funding.amount;
            funding.agreement_signed = dto.agreement_signed ?? funding.agreement_signed;
            funding.in_kind_support = dto.in_kind_support ?? funding.in_kind_support;
            funding.evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : funding.evidence;

            await _context.SaveChangesAsync();
            return Ok("Funding updated successfully.");
        }

        [HttpPut("updateEvents/{id}")]
        public async Task<IActionResult> UpdateEvents(int id, EventsDto dto)
        {
            var eventRecord = await _context.Events.FindAsync(id);
            if (eventRecord == null)
            {
                return NotFound("Event record not found.");
            }

            eventRecord.title = dto.title ?? eventRecord.title;
            eventRecord.event_date = dto.event_date ?? eventRecord.event_date;
            eventRecord.venue = dto.venue ?? eventRecord.venue;
            eventRecord.field = dto.field ?? eventRecord.field;
            eventRecord.panelist_details = dto.panelist_details ?? eventRecord.panelist_details;
            eventRecord.organizers = dto.organizers ?? eventRecord.organizers;
            eventRecord.audience = dto.audience ?? eventRecord.audience;
            eventRecord.participants_count = dto.participants_count ?? eventRecord.participants_count;
            eventRecord.evidence = !string.IsNullOrEmpty(dto.evidence) ? FileHelper.ConvertBase64ToByteArray(dto.evidence) : eventRecord.evidence;

            await _context.SaveChangesAsync();
            return Ok("Event updated successfully.");
        }


        [HttpDelete("deleteRicForm3/{id}")]
        public async Task<IActionResult> DeleteRicForm3(int id)
        {
            var ricForm = await _context.ric_form_3
                .Include(r => r.FacultyStartups)
                .Include(r => r.SpinOffs)
                .Include(r => r.Funding)
                .Include(r => r.Events)
                .FirstOrDefaultAsync(r => r.ric_form_3_id == id);

            if (ricForm == null)
            {
                return NotFound("RicForm3 record not found.");
            }

            _context.ric_form_3.Remove(ricForm);
            await _context.SaveChangesAsync();
            return Ok("RicForm3 and all related sub-forms deleted successfully.");
        }

        [HttpDelete("deleteFacultyStartup/{id}")]
        public async Task<IActionResult> DeleteFacultyStartup(int id)
        {
            var startup = await _context.FacultyStartups.FindAsync(id);
            if (startup == null)
            {
                return NotFound("Faculty Startup record not found.");
            }

            _context.FacultyStartups.Remove(startup);
            await _context.SaveChangesAsync();
            return Ok("Faculty Startup deleted successfully.");
        }

        [HttpDelete("deleteSpinOffs/{id}")]
        public async Task<IActionResult> DeleteSpinOffs(int id)
        {
            var spinoff = await _context.SpinOffs.FindAsync(id);
            if (spinoff == null)
            {
                return NotFound("SpinOff record not found.");
            }

            _context.SpinOffs.Remove(spinoff);
            await _context.SaveChangesAsync();
            return Ok("SpinOff deleted successfully.");
        }

        [HttpDelete("deleteFunding/{id}")]
        public async Task<IActionResult> DeleteFunding(int id)
        {
            var funding = await _context.Funding.FindAsync(id);
            if (funding == null)
            {
                return NotFound("Funding record not found.");
            }

            _context.Funding.Remove(funding);
            await _context.SaveChangesAsync();
            return Ok("Funding deleted successfully.");
        }

        [HttpDelete("deleteEvents/{id}")]
        public async Task<IActionResult> DeleteEvents(int id)
        {
            var eventRecord = await _context.Events.FindAsync(id);
            if (eventRecord == null)
            {
                return NotFound("Event record not found.");
            }

            _context.Events.Remove(eventRecord);
            await _context.SaveChangesAsync();
            return Ok("Event deleted successfully.");
        }


    }


}


