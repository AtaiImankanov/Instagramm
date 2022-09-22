using System.ComponentModel.DataAnnotations;

namespace LabInsta.ViewModels
{

        public class RegisterViewModel
        {

            [Required]
            [EmailAddress]
            [Display(Name = "Name")]
            public string Email { get; set; }
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }
            [Required]
            [DataType(DataType.Password)]
            [Compare("Password")]
            [Display(Name = "Confirm password")]
            public string ConfirmPassword { get; set; }

        }

}
