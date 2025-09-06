﻿
namespace E_Commerce.Core.Entites.Product
{
    public class Category : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Product> Products { get; set; } =  new List<Product>();
    }
}
