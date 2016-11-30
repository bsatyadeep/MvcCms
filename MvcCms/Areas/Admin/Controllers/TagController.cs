using System.Linq;
using System.Web.Mvc;
using MvcCms.Data;

namespace MvcCms.Areas.Admin.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagRepository _repository;

        public TagController() : this(new TagRepository())
        {

        }
        public TagController(ITagRepository repository)
        {
            _repository = repository;
        }

        // GET: Admin/Tag
        public ActionResult Index()
        {
            var tags = _repository.GetAll();
            return View(tags);
        }

        [HttpGet]
        public ActionResult Edit(string tag)
        {
            if (_repository.IsExists(tag))
            {
                return HttpNotFound();
            }
            return View(tag);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string tag, string newTag)
        {
            var tags = _repository.GetAll();

            if (!tags.Contains(tag))
            {
                return HttpNotFound();
            }
            if (tags.Contains(newTag))
            {
                return RedirectToAction("index");
            }
            if (!string.IsNullOrWhiteSpace(newTag))
            {
                ModelState.AddModelError("key", "new tag value can't be empty");
                return View(tag);
            }
            _repository.Edit(tag, newTag);
            return RedirectToAction("index");
        }
        //[HttpGet]
        //public ActionResult Delete(string tag)
        //{
        //    if (_repository.IsExists(tag))
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tag);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string tag)
        {
            if (!_repository.IsExists(tag))
            {
                return HttpNotFound();
            }
            if (!string.IsNullOrWhiteSpace(tag))
            {
                ModelState.AddModelError("key", "new tag value can't be empty");
                return View(tag);
            }
            _repository.Delete(tag);
            return RedirectToAction("index");
        }
    }
}