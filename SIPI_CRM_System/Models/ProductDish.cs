using System;
using System.Collections.Generic;

namespace SIPI_CRM_System.Models
{
    public partial class ProductDish
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int ProductId { get; set; }
        public int DishId { get; set; }

        public virtual Dish Dish { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
