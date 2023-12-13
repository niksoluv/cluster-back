namespace Entities.Models
{
    public class User: BaseModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Age { get; set;} = 0;
        public string Role { get; set; } = "User";
    }
}
