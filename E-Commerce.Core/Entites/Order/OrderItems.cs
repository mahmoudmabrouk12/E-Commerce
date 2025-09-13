namespace E_Commerce.Core.Entites.Order
{
    public class OrderItems : BaseEntity<int>
    {
        public OrderItems()
        {
                
        }
        public OrderItems(string productName, decimal price, int quentity, int productItemId, string mainImage)
        {
            ProductName = productName;
            Price = price;
            Quentity = quentity;
            ProductItemId = productItemId;
            MainImage = mainImage;
        }

        public string  ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quentity { get; set; }
        public int ProductItemId { get; set; }
        public string MainImage { get; set; }

       
    }

}