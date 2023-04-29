using System;
using System.Collections.Generic;

namespace SIPI_CRM_System.Models
{
    public partial class DailyOrder
    {
        public DailyOrder()
        {
            DailyOrderDishes = new HashSet<DailyOrderDish>();
        }

        public int Id { get; set; }
        public string? Price { get; set; }
        public int? Discount { get; set; }
        public DateTime OrderDateTime { get; set; }
        public int EmployeeId { get; set; }
        public int TableId { get; set; }
        public int ClientId { get; set; }

        public virtual Client Client { get; set; } = null!;
        public virtual Employee Employee { get; set; } = null!;
        public virtual Table Table { get; set; } = null!;
        public virtual ICollection<DailyOrderDish> DailyOrderDishes { get; set; }
    }
}
