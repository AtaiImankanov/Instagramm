namespace LabInsta.Models
{
    public class LikesModel
    {
        public int Id { get; set; }
        public int LikerId { get; set; }
        public User Liker { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
