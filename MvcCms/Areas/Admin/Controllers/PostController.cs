using System.Collections.Generic;
using System.Web.Mvc;
using MvcCms.Data;
using MvcCms.Models;

namespace MvcCms.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RoutePrefix("post")]
    public class PostController : Controller
    {
        private readonly IPostRepository _repository;

        public PostController() : this(new PostRepository())
        {

        }
        public PostController(IPostRepository repository)
        {
            _repository = repository;
        }

        //GET:Admin/Post
        public ActionResult Index()
        {
            //TODO: Retrive all our post
            var posts = _repository.GetAll();
            return View(posts);
        }
        // /admin/create/
        [HttpGet]
        [Route("create")]
        public ActionResult Create()
        {
            //TODO: retrive yje model from the data store
            var post = new Post { Tags = new List<string> { "test-1", "test-2" } };
            return View(post);
        }
        // /admin/create/
        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }
            //TODO: update model in data store
            _repository.Create(form);
            return RedirectToAction("index");
        }
        // /admin/edid/post-to-edit
        [HttpGet]
        [Route("edit/{postid}")]
        public ActionResult Edit(string postid)
        {
            //TODO: retrive the model from the data store

            var post = _repository.Get(postid);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // /admin/edid/post-to-edit
        [HttpPost]
        [Route("edit/{postid}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string postid, Post form)
        {
            var post = _repository.Get(postid);
            if (post == null)
            {
                return HttpNotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(form);
            }
            //TODO: update model in data store
            _repository.Edit(postid, post);
            return RedirectToAction("index");
        }
    }
}