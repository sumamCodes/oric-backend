using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos
{
    public class RicForm3Dto
    {
        [Required] public int ric_form_3_id { get; set; }
        [Required] public int dataoric_id { get; set; }
        [Required] public required string full_name { get; set; }
        [Required] public required string designation { get; set; }
        [Required] public required string department { get; set; }
        [Required] public required string email { get; set; }
        [Required] public int number_faculty_led_startups { get; set; }
        [Required] public int number_spin_offs { get; set; }
        [Required] public int jobs_created_retained { get; set; }
        [Required] public int students_placed { get; set; }
        [Required] public int participation_count { get; set; }

        // Optional sub-forms (nullable)
        public List<FacultyStartupsDto>? FacultyStartups { get; set; }
        public List<SpinOffsDto>? SpinOffs { get; set; }
        public List<FundingDto>? Funding { get; set; }
        public List<EventsDto>? Events { get; set; }
    }
}
