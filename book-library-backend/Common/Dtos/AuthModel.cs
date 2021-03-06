using System.ComponentModel.DataAnnotations;

namespace Common.Dtos
{
    public class AuthModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; init; } = string.Empty;
        [Required]
        [MinLength(6)]
        public string Password { get; init; } = string.Empty;
    }
}