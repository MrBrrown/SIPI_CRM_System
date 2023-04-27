using System;
using System.Collections.Generic;

namespace SIPI_CRM_System.Models
{
    public partial class Client
    {
        public Client()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? Email { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
