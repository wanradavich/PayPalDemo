using System.ComponentModel.DataAnnotations;

namespace PayPalDemo.ViewModels
{
    public class RoleVM
    {
        [Required]
        [Display(Name = "Role Name")]
        public string? RoleName { get; set; }

        [Required]
        [Display(Name = "Id")]
        public string Id { get; set; }

    }


}

