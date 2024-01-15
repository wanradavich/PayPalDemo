using System.ComponentModel.DataAnnotations;

namespace PayPalDemo.ViewModels
{
    public class UserVM
    {
        [Required]
        [Display(Name = "Email")]
        public string? Email { get; set; }

    }
}
