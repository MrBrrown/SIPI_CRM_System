using System;
using System.Collections.Generic;

namespace SIPI_CRM_System.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDishes = new HashSet<OrderDish>();
        }

        public int Id { get; set; }
        public decimal? Price { get; set; }
        public int? Discount { get; set; }
        public DateTime OrderDateTime { get; set; }
        public int? ClientId { get; set; }
        public int? TableId { get; set; }
        public int? EmployeeId { get; set; }

        public virtual Client? Client { get; set; }
        public virtual Employee? Employee { get; set; }
        public virtual Table? Table { get; set; }
        public virtual ICollection<OrderDish> OrderDishes { get; set; }
    }
}
