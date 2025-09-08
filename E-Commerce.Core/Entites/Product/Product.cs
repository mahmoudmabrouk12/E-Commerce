
namespace E_Commerce.Core.Entites.Product
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }

        public int CategoryId { get; set; }
        public  virtual Category category { get; set; }
        public virtual ICollection<Photo>  photos { get; set; } = new List<Photo>();



    }
}
