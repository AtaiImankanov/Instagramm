using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LabInsta.Models;
using LabInsta.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace LabInsta.Controllers
{
    public class PostsController : Controller
    {
        private readonly InstaContext _context;
        private readonly UserManager<User> _userManager;

        public PostsController(InstaContext context, UserManager<User> userManager )
        {
            _userManager= userManager;
            _context = context;
        }

        // GET: Posts
        public async Task<IActionResult> Index(string LoginName)
        {
            if (LoginName != null)
            {
                return  RedirectToAction("SearchByLogin",new {login=LoginName});
            }
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            int idUser = Convert.ToInt32(_userManager.GetUserId(currentUser));
            var user = _userManager.Users.FirstOrDefault(x => x.Id == idUser);
            List<Post> postsModel= new List<Post>();
            foreach(var u in _context.FollowsModels.ToList())
            {
                if(u.FollowerId == idUser)
                {
                  foreach(var post in _context.Post.ToList())
                    {
                        if(post.UserId == _userManager.Users.FirstOrDefault(x => x.Id == u.FollowsId).Id)
                        {
                              postsModel.Add(post);
                        }
                    } 
                }
            }
            foreach(var post in _context.Post)
            {
                if (post.UserId == idUser)
                {
                    postsModel.Add(post);
                }
            }
            FeedViewModel model = new FeedViewModel
            {
              Posts= postsModel
            };
            return  View(model);
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Pic,Description,AmountOfLikes,AmountOfComments,UserId")] Post post)
        {
            if (ModelState.IsValid)
            {
                System.Security.Claims.ClaimsPrincipal currentUser = this.User;
                int idUser = Convert.ToInt32(_userManager.GetUserId(currentUser));
                var user = _userManager.Users.FirstOrDefault(x => x.Id == idUser);
                post.AmountOfComments = 0;
                post.AmountOfLikes = 0;
                post.TimeCreated= DateTime.Now;
                post.UserId=idUser; 
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Pic,Description,AmountOfLikes,AmountOfComments,UserId")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Post.FindAsync(id);
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.Id == id);
        }

        [HttpGet]
        public IActionResult Follow(int id)
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            int idUser = Convert.ToInt32(_userManager.GetUserId(currentUser));          
            var userfollows = _context.Users.FirstOrDefault(u => u.Id == id);
            var userfollower = _userManager.Users.FirstOrDefault(x => x.Id == idUser);
            foreach(var follow in _context.FollowsModels.ToList())
            {
                if(follow.FollowerId==userfollower.Id && follow.FollowsId == userfollows.Id)
                {
                    return Redirect($"https://localhost:44348/Posts/OtherUser/{userfollows.Id}");
                }
            }
            FollowsModel fm = new FollowsModel();
            userfollows.Subscribers++;
            userfollower.Follows++;
            fm.FollowsId = userfollows.Id;
            fm.FollowerId = userfollower.Id;
            _context.FollowsModels.Add(fm);
            _context.SaveChanges();
            return Redirect($"https://localhost:44348/Posts/OtherUser/{userfollows.Id}");
        }
        [HttpGet]
        public IActionResult YourPage()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            int idUser = Convert.ToInt32(_userManager.GetUserId(currentUser));
            var user = _userManager.Users.FirstOrDefault(x => x.Id == idUser);
            List<Post> posts = new List<Post>();
            foreach(var p in _context.Post)
            {
                if (p.UserId == idUser)
                {
                    posts.Add(p);
                }
            }
            YourPageViewModel yourPageViewModel = new YourPageViewModel
            {
                Posts = posts,
                User = user
            };
            return View(yourPageViewModel);
        }
        [HttpGet]
        public IActionResult Like(int id)
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            int idUser = Convert.ToInt32(_userManager.GetUserId(currentUser));
            var Liker = _userManager.Users.FirstOrDefault(x => x.Id == idUser);
            var post = _context.Post.FirstOrDefault(x => x.Id == id);

            foreach (var item in _context.LikesModels.ToList())
            {
                if (item.LikerId==Liker.Id && item.PostId==post.Id)
                {
                    return RedirectToAction("Index", "Posts");
                }
            }
            LikesModel lm = new LikesModel();
            post.AmountOfLikes++;
            _context.Update(post);
            lm.PostId = post.Id;
            lm.LikerId = Liker.Id;
            _context.LikesModels.Add(lm);
            _context.SaveChanges();
            return RedirectToAction("Index", "Posts");

        }
        public IActionResult CommentPost(int id)
        {
            ViewBag.PostId = id;
            var post = _context.Post.FirstOrDefault(x => x.Id == id);
            List<Comment> coms= (_context.Comments.Where(x => x.PostId == id)).ToList();
            PostNCommentsViewModel pcm = new PostNCommentsViewModel()
            {
                Post = post,
                Comments = coms
            };
            return View(pcm);
        }
        [HttpGet]
        public IActionResult SearchByLogin(string login)
        {
            IQueryable<User> users = _context.Users;
            List<User> usersmodel= new List<User>();
            foreach(var u in users)
            {
                if (u.UserName.Contains(login) || u.Email.Contains(login))
                {
                    usersmodel.Add(u);
                }
                if(u.InfoUser != null)
                {
                    if (u.InfoUser.Contains(login))
                    {
                        usersmodel.Add(u);
                    }
                }
                if (u.FullName != null)
                {
                    if (u.FullName.Contains(login))
                    {
                        usersmodel.Add(u);
                    }
                }
            }
            return View(usersmodel);

        }
        [HttpGet]
        public IActionResult OtherUser(int id)
        {
            IQueryable<User> users = _context.Users;
           var user = users.FirstOrDefault(t => t.Id==id);
            var posts= _context.Post.Where(p=> p.UserId==user.Id).ToList();
            YourPageViewModel model = new YourPageViewModel
            {
                User = user,
                Posts = posts
            };
            return View(model);
        }
    }
}
