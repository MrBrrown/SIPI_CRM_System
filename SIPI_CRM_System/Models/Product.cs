using System;
using System.Collections.Generic;

namespace SIPI_CRM_System.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductDishes = new HashSet<ProductDish>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public decimal Amount { get; set; }
        public int LifeTime { get; set; }
        public DateTime DeliveryDateTime { get; set; }

        public virtual ICollection<ProductDish> ProductDishes { get; set; }
    }
}
