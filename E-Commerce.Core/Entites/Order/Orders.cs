namespace E_Commerce.Core.Entites.Order
{
    public class Orders:BaseEntity<int>
    {
        public Orders()
        {
            
        }
        public Orders(string buyerEmail, decimal subTotal, ShippingAddress shippingAddress,
            IReadOnlyList<OrderItems> orderItems, DelivaryMethod delivaryMethod )
        {
            BuyerEmail = buyerEmail;
            SubTotal = subTotal;
            this.shippingAddress = shippingAddress;
            this.orderItems = orderItems;
            this.delivaryMethod = delivaryMethod;
        }

        public string BuyerEmail { get; set; }
        public decimal SubTotal { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public ShippingAddress shippingAddress { get; set; }
        public IReadOnlyList<OrderItems> orderItems { get; set; } 
        public DelivaryMethod delivaryMethod { get; set; }
        public Status Status { get; set; } = Status.Pending;

        public decimal GetTotal()
        {
            return SubTotal + delivaryMethod.Price;
        }
    }
}
