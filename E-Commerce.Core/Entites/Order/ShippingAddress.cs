namespace E_Commerce.Core.Entites.Order
{
    public class ShippingAddress : BaseEntity<int>
    {
        public ShippingAddress()
        {
        }

        public ShippingAddress(string firstName, string lastName, string streat, string zipCode, string city, string status)
        {
            FirstName = firstName;
            LastName = lastName;
            Streat = streat;
            ZipCode = zipCode;
            City = city;
            Status = status;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Streat { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Status { get; set; }
    }
}