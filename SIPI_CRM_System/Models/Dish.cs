using System;
using System.Collections.Generic;

namespace SIPI_CRM_System.Models
{
    public partial class Dish
    {
        public Dish()
        {
            DailyOrderDishes = new HashSet<DailyOrderDish>();
            OrderDishes = new HashSet<OrderDish>();
            ProductDishes = new HashSet<ProductDish>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public decimal? Mass { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<DailyOrderDish> DailyOrderDishes { get; set; }
        public virtual ICollection<OrderDish> OrderDishes { get; set; }
        public virtual ICollection<ProductDish> ProductDishes { get; set; }
    }
}
