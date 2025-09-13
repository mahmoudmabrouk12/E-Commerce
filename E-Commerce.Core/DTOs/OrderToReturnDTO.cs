using E_Commerce.Core.Entites.Order;

namespace E_Commerce.Core.DTOs
{
    public record OrderToReturnDTO
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderDate { get; set; }
        public ShippingAddress shippingAddress { get; set; }
        public IReadOnlyList<OrderItemsDTO> orderItems { get; set; }
        public string DelivaryMethod  { get; set; }
        public string Status  { get; set; }

    }
    public record OrderItemsDTO
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quentity { get; set; }
        public int ProductItemId { get; set; }
        public string MainImage { get; set; }

    }

}
