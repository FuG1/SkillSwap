using System.ComponentModel.DataAnnotations;

namespace learn_asp.Models
{
    public class RegisterModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [MaxLength(32, ErrorMessage = "Username must be at most 20 characters")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; } = string.Empty;
    }
}