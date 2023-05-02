using System;
using System.Collections.Generic;

namespace SIPI_CRM_System.Models
{
    public partial class Employee
    {
        public Employee()
        {
            DailyOrders = new HashSet<DailyOrder>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool IsAdmin { get; set; }
        public int PositionId { get; set; }

        public virtual Position Position { get; set; } = null!;
        public virtual ICollection<DailyOrder> DailyOrders { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
