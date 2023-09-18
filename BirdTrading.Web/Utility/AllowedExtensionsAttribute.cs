using System.ComponentModel.DataAnnotations;

namespace BirdTrading.Web.Utility
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        public readonly string[] _extenstions;
        public AllowedExtensionsAttribute(string[] extenstions)
        {
            _extenstions = extenstions; 
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null) { 
                var extension = Path.GetExtension(file.FileName);   
                if (!_extenstions.Contains(extension.ToLower())) 
                {
                    return new ValidationResult("This photo extension is not allowed!");    
                }
            }
            return ValidationResult.Success;
        }
    }
}
