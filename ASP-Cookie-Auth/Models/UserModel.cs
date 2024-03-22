namespace ASP_Cookie_Auth.Models;

public class UserModel
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public DateTime CreatedAt { get; set; }
}