namespace E_Commerce.Core.Entites.Order
{
    public class DelivaryMethod : BaseEntity<int>
    {
        public DelivaryMethod( )
        {
            
        }
        public DelivaryMethod(string name, string description, string delivaryTime, decimal price)
        {
            Name = name;
            Description = description;
            DelivaryTime = delivaryTime;
            Price = price;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string DelivaryTime { get; set; }
        public decimal Price { get; set; }



    }
}