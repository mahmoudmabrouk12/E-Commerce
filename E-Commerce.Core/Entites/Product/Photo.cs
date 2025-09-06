using System.ComponentModel.DataAnnotations.Schema;


namespace E_Commerce.Core.Entites.Product
{
    public class Photo : BaseEntity<int>
    {



        public string Imagename { get; set; }
        [ForeignKey(nameof(ProductId))]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}