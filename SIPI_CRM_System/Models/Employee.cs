using System;
using System.Collections.Generic;

namespace SIPI_CRM_System.Models
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Position { get; set; } = null!;
        public bool IsAdmin { get; set; }
    }
}
