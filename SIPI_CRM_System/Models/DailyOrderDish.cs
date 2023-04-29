using System;
using System.Collections.Generic;

namespace SIPI_CRM_System.Models
{
    public partial class DailyOrderDish
    {
        public int Id { get; set; }
        public int DishId { get; set; }
        public int DailyOrderId { get; set; }

        public virtual DailyOrder DailyOrder { get; set; } = null!;
        public virtual Dish Dish { get; set; } = null!;
    }
}
