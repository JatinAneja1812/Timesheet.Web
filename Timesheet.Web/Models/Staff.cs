using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimesheetApp.Web.Models
{
    public class Staff
    {
        public int StaffId { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Forename { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
