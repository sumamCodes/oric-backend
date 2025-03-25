using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApi.Models
{
    public class RicForm3
    {
        [Key]
        public int ric_form_3_id { get; set; }

        [Required]
        public int dataoric_id { get; set; }

        //[ForeignKey("dataoric_id")]
        //public required dataoric dataoric { get; set; }   

        [Required]
        public required string full_name { get; set; }

        [Required]
        public required string designation { get; set; }

        [Required]
        public required string department { get; set; }

        [Required]
        [EmailAddress]
        public required string email { get; set; }

        public int number_faculty_led_startups { get; set; }
        public int number_spin_offs { get; set; }
        public int jobs_created_retained { get; set; }
        public int students_placed { get; set; }
        public int participation_count { get; set; }

        // Navigation Properties
        public ICollection<FacultyStartups> FacultyStartups { get; set; } = new List<FacultyStartups>();
        public ICollection<SpinOffs> SpinOffs { get; set; } = new List<SpinOffs>();
        public ICollection<Funding> Funding { get; set; } = new List<Funding>();
        public ICollection<Events> Events { get; set; } = new List<Events>();
    }
}
