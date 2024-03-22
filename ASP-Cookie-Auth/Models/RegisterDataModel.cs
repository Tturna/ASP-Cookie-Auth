using System.ComponentModel.DataAnnotations;
using ASP_Cookie_Auth.Validators;

namespace ASP_Cookie_Auth.Models;

public class RegisterDataModel
{
    [Required]
    [MinLength(3)]
    [MaxLength(20)]
    public string Username { get; set; }
    [Required]
    [ValidPassword] // Custom validation attribute
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}