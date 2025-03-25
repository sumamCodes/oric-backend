// Models/login.cs
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Login
    {
        [Key]
        public int dataoric_id { get; set; }

        public required string email { get; set; }
        public required string name { get; set; }
        public required string password { get; set; }
        public required string role { get; set; }
        public int? department_id { get; set; } // Nullable for admin who may not belong to any department
    }
}
