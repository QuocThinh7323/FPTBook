using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;

namespace FPTBook.Models
{
    public class SetPasswordViewModel
    {
        public string UID { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} character long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Display(Name ="Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirm password do not match.")]
        public string ConfirmPassword { get; set;}
    }
}
