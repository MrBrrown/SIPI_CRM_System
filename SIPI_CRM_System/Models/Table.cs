using System;
using System.Collections.Generic;

namespace SIPI_CRM_System.Models
{
    public partial class Table
    {
        public Table()
        {
            DailyOrders = new HashSet<DailyOrder>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public int Places { get; set; }

        public virtual ICollection<DailyOrder> DailyOrders { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
