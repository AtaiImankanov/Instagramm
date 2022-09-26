using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LabInsta.ViewModels
{

        public class RegisterViewModel
        {

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            [Remote(action: "EmailValid", controller: "Posts")]
        public string Email { get; set; }
        public string FullName { get; set; }
            [Required]
        [Remote(action: "UserNameValid", controller: "Posts")]
        public string UserName { get; set; }
        [Required]
        public string Avatar { get; set; }
            
            public string InfoUser { get; set; }
            
            public string PhoneNumber { get; set; }
            
            public string Sex { get; set; }
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
