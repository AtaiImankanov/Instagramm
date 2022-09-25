using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LabInsta.Models;

namespace LabInsta.Models
{
    public class InstaContext: IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<LabInsta.Models.Post> Post { get; set; }
        public DbSet<LabInsta.Models.FollowsModel> FollowsModels { get; set; }
        public DbSet<LabInsta.Models.LikesModel> LikesModels { get; set; }
        public DbSet<LabInsta.Models.Comment> Comments { get; set; }
        public InstaContext(DbContextOptions<InstaContext> options) : base(options)
        {

        }
      
    }
}
