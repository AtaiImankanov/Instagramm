using Microsoft.AspNetCore.Identity;

namespace LabInsta.Models
{
    public class User : IdentityUser<int>
    {

        public string Avatar { get; set; }
        public int Subscribers { get; set; }
        public int AmountOfPosts { get; set; }
        public int Follows { get; set; }

        public string FullName  { get; set; }
        public string InfoUser { get; set; }
        public string Sex { get; set; }
    }
}
