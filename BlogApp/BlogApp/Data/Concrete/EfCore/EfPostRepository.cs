using System;
using BlogApp.Data.Abstract;
using BlogApp.Entity;

namespace BlogApp.Data.Concrete.EfCore;

public class EfPostRepository : IPostRepository
{
    private BlogContext _context;
    public EfPostRepository(BlogContext context)
    {
        _context = context;
    }
    public IQueryable<Post> Posts => _context.Posts;

    public void CreatePost(Post post)
    {
        _context.Posts.Add(post);
        _context.SaveChanges();
    }
}
