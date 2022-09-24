
using System;

namespace LabInsta.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Pic { get; set; }
        public string Description { get; set; }
        public int AmountOfLikes { get; set; }
        public int AmountOfComments { get; set; }

        public DateTime TimeCreated { get; set; }
        
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
