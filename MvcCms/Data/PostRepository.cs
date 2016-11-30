using System;
using System.Collections.Generic;
using MvcCms.Models;

namespace MvcCms.Data
{
    public class PostRepository : IPostRepository
    {
        public void Create(Post post)
        {
            throw new NotImplementedException();
        }

        public void Edit(string id, Post updatedPost)
        {
            throw new NotImplementedException();
        }

        public Post Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}