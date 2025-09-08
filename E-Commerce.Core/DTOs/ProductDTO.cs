using E_Commerce.Core.Entites.Product;
using Microsoft.AspNetCore.Http;

namespace E_Commerce.Core.DTOs
{
    public record ProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<Photo> photos { get; set; } = new List<Photo>();
        public string CategoryName { get; set; }

    }
    public record PhotoDTO
    {
        public string Imagename { get; set; }
        public int ProductId { get; set; }

    }
    public record AddProductDTO 
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }

        public int CategoryId { get; set; }

        public IFormFileCollection Photos {  get; set; }    

    }
    public record UpdateProductDTO : AddProductDTO
    {
        public int Id { get; set; }
    }

}
