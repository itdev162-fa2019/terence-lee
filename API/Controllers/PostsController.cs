using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly DataContext context;

        public PostsController(DataContext context)
        {
            this.context = context;
        }



        /// <summary>
        /// GET api/posts 
        /// </summary>
        /// <returns>A list of posts</returns>
        [HttpGet]
        public ActionResult<List<Post>> Get()
        {
            return this.context.Posts.ToList();
        }

        /// <summary>
        /// GET api/posts/{id}
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns>A single post</returns>
        [HttpGet("{id}")]
        public ActionResult<Post> GetById(Guid id)
        {
            return this.context.Posts.Find(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request">JSON request containing post fields</param>
        /// <returns>A new post</returns>
        [HttpPost]
        public ActionResult<Post> Create([FromBody]Post request)
        {
            var post = new Post {
                Id = request.Id,
                Body = request.Body,
                Title = request.Title,
                Date = request.Date
            };

            context.Posts.Add(post);
            var success = context.SaveChanges() > 0;

            if(success)
            {
                return post;
            }

            throw new Exception("Error creating post");
        }

        [HttpPut]
        public ActionResult<Post> Update([FromBody]Post request)
        {
            var post = context.Posts.Find(request.Id);

            if(request == null)
            {
                throw new Exception("Couldn\'t find post");
            }

            //Update the post propertieswith request values, if present
            post.Title = request.Title != null ? request.Title:post.Title;
            post.Body = request.Body != null ? request.Body:post.Body;
            post.Date = request.Date != null ? request.Date:post.Date;

            var success = context.SaveChanges() > 0;

            if(success)
            {
                return post;
            }

            throw new Exception("Error updating post");
        }

        /// <summary>
        /// DELETE api/posts/{id}
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns>True, if successful</returns>
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(Guid id)
        {
            var post = context.Posts.Find(id);

            if(post == null)
            {
                throw new Exception("Couldn\'t find post!");
            }

            context.Remove(post);

            var success = context.SaveChanges() > 0;

            if(success)
            {
                return true;
            }

            throw new Exception("Error deleting post.");
        }
    }
}