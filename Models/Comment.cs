using System;

namespace LabInsta.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Descripton { get; set; }
        public DateTime DateCreated { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
