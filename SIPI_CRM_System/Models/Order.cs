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
        public DateTime OrderDateTime { get; set; }
        public int ClientId { get; set; }
        public int TableId { get; set; }

        public virtual Client Client { get; set; } = null!;
        public virtual Table Table { get; set; } = null!;
        public virtual ICollection<OrderDish> OrderDishes { get; set; }
    }
}
