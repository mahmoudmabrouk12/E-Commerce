using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Core.Entites.User
{
    public class Address : BaseEntity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Streat { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Status { get; set; }

        [ForeignKey(nameof(AppUserId))]
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }






    }
}