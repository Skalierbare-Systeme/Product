using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using postsPraktikum.Data;
using postsPraktikum.Models;
using postsPraktikum.Models.Entities;

namespace postsPraktikum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public PostsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // --- NEW: Endpoint to get all public posts ---
        [HttpGet("public")]
        public IActionResult GetPublicPosts()
        {
            // Filter posts using a Where clause to find posts where IsPrivate is false
            var posts = dbContext.Posts.Where(p => !p.IsPrivate).ToList();
            return Ok(posts);
        }
        [HttpGet]
        public IActionResult GetPosts()
        {
            return Ok(dbContext.Posts.ToList());
        }
        // --- NEW: Endpoint to get posts for a specific user ---
        [HttpGet("user/{userId:guid}")]
        public IActionResult GetPostsByUser(Guid userId)
        {
            // Filter posts using a Where clause to find posts matching the userId
            var posts = dbContext.Posts.Where(p => p.UserId == userId).ToList();
            
            if (posts == null)
            {
                // Return an empty list instead of NotFound if the user simply has no posts
                return Ok(new List<Post>());
            }
            
            return Ok(posts);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetPostById(Guid id)
        {
            var post = Ok(dbContext.Posts.Find(id));

            if (post == null) return NotFound();

            return Ok(post);
        }

        [HttpPost]
        public IActionResult UploadPost(PostDto uploadPostDto)
        {
            var postEntity = new Post() 
            {
                PostTitle = uploadPostDto.PostTitle,
                PostDescription = uploadPostDto.PostDescription,
                PostText = uploadPostDto.PostText,
                PostCreationDate = uploadPostDto.PostCreationDate,
                Photos = uploadPostDto.Photos,
                UserId = uploadPostDto.UserId,
                IsPrivate = uploadPostDto.IsPrivate
            };

            dbContext.Posts.Add(postEntity);
            dbContext.SaveChanges();

            return Ok(postEntity);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdatePost(Guid id, PostDto updatePostDto)
        {
            var post = dbContext.Posts.Find(id);
            if (post == null) return NotFound();

            post.PostTitle = updatePostDto.PostTitle;
            post.PostDescription = updatePostDto.PostDescription;
            post.PostText = updatePostDto.PostText;
            post.PostCreationDate = updatePostDto.PostCreationDate;
            post.Photos = updatePostDto.Photos;
            post.IsPrivate = updatePostDto.IsPrivate;
            dbContext.SaveChanges();

            return Ok(post);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeletePost(Guid id)
        {
            var post = dbContext.Posts.Find(id);
            if (post == null) return NotFound();

            dbContext.Posts.Remove(post);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}
