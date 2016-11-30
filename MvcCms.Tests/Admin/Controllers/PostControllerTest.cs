using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcCms.Areas.Admin.Controllers;
using MvcCms.Data;
using MvcCms.Models;
using Telerik.JustMock;

namespace MvcCms.Tests.Admin.Controllers
{
    [TestClass]
    public class PostControllerTest
    {
        [TestMethod]
        public void Edid_GetRequestSendsPostToView()
        {
            var id = "test-post";
            var repo = Mock.Create<IPostRepository>();
            var controller = new PostController(repo);

            Mock.Arrange(() => repo.Get(id)).Returns(new Post { Id = id });

            var result = (ViewResult)controller.Edit(id);

            var model = (Post)result.Model;

            Assert.AreEqual(id, model.Id);
        }
        [TestMethod]
        public void Edid_GetRequestNotFound()
        {
            var id = "test-post";
            var repo = Mock.Create<IPostRepository>();
            var controller = new PostController(repo);

            Mock.Arrange(() => repo.Get(id)).Returns((Post)null);

            var result = controller.Edit(id);

            Assert.IsTrue(result is HttpNotFoundResult);
        }
        [TestMethod]
        public void Edid_PostRequestNotFound()
        {
            var id = "test-post";
            var repo = Mock.Create<IPostRepository>();
            var controller = new PostController(repo);

            Mock.Arrange(() => repo.Get(id)).Returns((Post)null);

            var result = controller.Edit(id, new Post());

            Assert.IsTrue(result is HttpNotFoundResult);
        }
        [TestMethod]
        public void Edid_PostRequestSendsPostToView()
        {
            var id = "test-post";
            var repo = Mock.Create<IPostRepository>();
            var controller = new PostController(repo);

            Mock.Arrange(() => repo.Get(id)).Returns(new Post { Id = id });
            controller.ModelState.AddModelError("key", "error message");
            var result = (ViewResult)controller.Edit(id, new Post { Id = "test-post-2" });

            var model = (Post)result.Model;

            Assert.AreEqual("test-post-2", model.Id);
        }
        [TestMethod]
        public void Edid_PostRequestCallsEdidAndRedirects()
        {
            var repo = Mock.Create<IPostRepository>();
            var controller = new PostController(repo);

            Mock.Arrange(() => repo.Edit(Arg.IsAny<string>(), Arg.IsAny<Post>())).MustBeCalled();

            var result = controller.Edit("test-post-1", new Post { Id = "test-post-2" });

            Mock.Assert(repo);

            Assert.IsTrue(result is RedirectToRouteResult);
        }
    }
}
