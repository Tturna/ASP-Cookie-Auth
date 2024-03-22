using System.ComponentModel.DataAnnotations;

namespace ASP_Cookie_Auth.Models;

public class LoginDataModel
{
    [Required]
    public string Username { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}