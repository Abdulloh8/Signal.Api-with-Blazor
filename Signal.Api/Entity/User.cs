using Microsoft.AspNetCore.Identity;

namespace Signal.Api.Entity
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber {get; set;}
        public Guid GrupId { get; set; }

    }
}
