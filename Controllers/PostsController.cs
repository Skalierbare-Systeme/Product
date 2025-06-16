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

        [HttpGet]
        public IActionResult GetPosts()
        {
            return Ok(dbContext.Posts.ToList());
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
                Photos = uploadPostDto.Photos
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
