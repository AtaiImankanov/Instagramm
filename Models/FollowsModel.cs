namespace LabInsta.Models
{
    public class FollowsModel
    {
        public int Id { get; set; }
        public int FollowerId { get; set; }
        public User Follower { get; set; }

        public int FollowsId { get; set; }
        public User Follows { get; set; }
    }
}
