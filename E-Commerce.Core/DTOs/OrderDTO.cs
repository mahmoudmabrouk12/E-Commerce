
namespace E_Commerce.Core.DTOs
{
    public record OrderDTO
    {
        public int delivaryMethodeId { get; set; }
        public string basketId { get; set; }
        public ShippAdressDTO shippAdressDTO { get; set; }
    }
    public record ShippAdressDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Streat { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Status { get; set; }

    }
}
