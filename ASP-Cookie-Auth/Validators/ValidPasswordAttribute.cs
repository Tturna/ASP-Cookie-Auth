using System.ComponentModel.DataAnnotations;

namespace ASP_Cookie_Auth.Validators;

public class ValidPasswordAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var password = value as string ?? string.Empty;
        
        if (password.Length < 8)
        {
            return new ValidationResult("Password must be at least 8 characters long.");
        }
        
        if (password.Length > 60)
        {
            return new ValidationResult("Password must be at most 60 characters long.");
        }
        
        // This is equivalent:
        // if (!password.Any(c => char.IsUpper(c)))
        if (!password.Any(char.IsUpper))
        {
            return new ValidationResult("Password must contain at least one uppercase letter.");
        }
        
        if (!password.Any(char.IsLower))
        {
            return new ValidationResult("Password must contain at least one lowercase letter.");
        }
        
        if (!password.Any(char.IsDigit))
        {
            return new ValidationResult("Password must contain at least one number.");
        }
        
        if (!password.Any(c => !char.IsLetterOrDigit(c)))
        {
            return new ValidationResult("Password must contain at least one special character.");
        }

        return ValidationResult.Success;
    }
}