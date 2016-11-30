using System.Collections.Generic;
using MvcCms.Models;

namespace MvcCms.Data
{
    public interface IPostRepository
    {
        Post Get(string id);
        void Edit(string id, Post updatedPost);
        void Create(Post post);
        IEnumerable<Post> GetAll();
    }
}