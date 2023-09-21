namespace Signal.Api.Models
{
    public class UserDto
    {
        
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid? GrupId { get; set; }
    }
}
