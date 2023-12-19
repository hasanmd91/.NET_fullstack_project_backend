using Ecom.Core.src.Enum;

namespace Ecom.Core.src.Entity
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; } = Role.User;
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
        public IEnumerable<Order> Orders { get; set; }

    }
}