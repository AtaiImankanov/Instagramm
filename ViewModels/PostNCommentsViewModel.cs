using LabInsta.Models;
using System.Collections.Generic;

namespace LabInsta.ViewModels
{
    public class PostNCommentsViewModel
    {
        public Post Post { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
