using Ecom.Core.src.Entity;
using Ecom.Core.src.Enum;

namespace Ecom.Service.src.DTO
{
    public class UserReadDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public Role Role { get; set; }
        public IEnumerable<Order> Orders { get; set; }

    }


    public class UserUpdateDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Avatar { get; set; }
        public string? Address { get; set; }
        public string? Zip { get; set; }
        public string? City { get; set; }

    }


    public class UserCreateDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
    }


    public class LoginDTO
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }


    public class ChangeRoleDTO
    {
        public Role Role { get; set; } = Role.Admin;
    }



}