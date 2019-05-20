using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GuestBookApp.Models;
using GuestBookApp.Data;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace GuestBookApp.Controllers
{
    [Authorize]
    [Route("api/posts")]
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Post> GetPost()
        {

            return _context.Post.OrderByDescending(p => p.CreatedDate); ;
        }

        // POST: api/Posts
        [HttpPost]
        public async Task<IActionResult> PostPost([FromBody]JObject body)
        {
            var query = from st in _context.Users
                        where st.Id == this.User.FindFirstValue(ClaimTypes.NameIdentifier)
                        select st.UserName;

            var user = query.First();

            
            Post post = new Post();
            post.Message = body.GetValue("message").Value<string>();
            post.Likes = 0;
            post.CreatedDate = DateTime.Now;
            post.CreatedBy = user;

            _context.Post.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPost", new { id = post.Id }, post);
        }



        // POST: api/Posts
        [HttpPut]
        public async Task<IActionResult> PutPost([FromBody]JObject body)
        {
            Post post = _context.Post.First(a => a.Id == body.GetValue("id").Value<int>());
            post.Likes++;
            _context.SaveChanges();
          
            return NoContent();
        }

    }
}
