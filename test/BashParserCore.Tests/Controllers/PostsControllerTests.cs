using BashParserCore.Controllers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using BashParserCore.Data.Repositories;
using BashParserCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using BashParserCore.Services;
using BashParserCore.Data;
using Microsoft.AspNetCore.Mvc;

namespace BashParserCore.Tests.Controllers
{
    [TestFixture]
    public class PostsControllerTests
    {
        private Mock<IRepository<Post>> mockRepo;
        private Mock<ICurrentUserService> mockUserService;
        private PostsController postsController;
        public PostsControllerTests()
        {
            mockRepo = new Mock<IRepository<Post>>();
            mockRepo.Setup(repo => repo.getElementList()).Returns(Task.FromResult(getPostsList()));
            mockUserService = new Mock<ICurrentUserService>();
            mockUserService.Setup(s => s.getCurrentUser()).Returns(new ApplicationUser { Id = "1234", UserName = "TestUser" });
            postsController = new PostsController(mockUserService.Object, mockRepo.Object);
        }

        [Test]
        public void Index_DoesReturnList()
        {
            IActionResult result = postsController.Index();
            var actionResult = result as ViewResult;

            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<IEnumerable<Post>>(actionResult.Model);
        }

        [TestCase(12)]
        public void Details_ReturnsValidModel_IfIdInvalid(int? id)
        {
            mockRepo.Setup(p => p.getElement(id.Value)).Returns(getPost(id.Value));

            IActionResult result = postsController.Details(id);
            var actionResult = result as ViewResult;

            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<Post>(actionResult.Model);
        }

        [TestCase(null)]
        public void Details_ReturnsInvalidModel_IfIdInvalid(int? id)
        {

            var result = postsController.Details(id);
            var actionResult = result as ViewResult;

            Assert.IsNull(actionResult);
        }
        [Test]
        public void CreateGet_ReturnsConstraindedView()
        {
            IActionResult result = postsController.Create();
            var actionResult = result as ViewResult;

            //  Assert.That(result, Is.EqualTo("Create"));
        }

        [TestCase(12, "01.01.2000", "PostName", "1111", "SampleText")]
        public void CreatePost_RedirectsToController_WithValidModel(int Id, string Date, string postName, string rating , string text)
        {
            var result = postsController.Create(new Post{ Id = Id, Date = Date, PostName = postName, Rating = rating, Text = text }).Result;
            var actionResult = result as RedirectToActionResult;

            Assert.That(actionResult.ActionName, Is.EqualTo("Index"));
        }

        [TestCase(12)]
        public void EditGet_IsModelValid_WithValidIncome(int? id)
        {
            mockRepo.Setup(p => p.getElement(id.Value)).Returns(getPost(id.Value));

            var result = postsController.Edit(id);
            var actionResult = result as ViewResult;

            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Model);
        }

        [TestCase(12)]
        public void EditGet_IsModelValid_WithInvalidIncome(int? id)
        {
            Post post = null;
            mockRepo.Setup(p => p.getElement(id.Value)).Returns(post);

            var result = postsController.Edit(id);
            var actionResult = result as ViewResult;
            var actionResult404 = result as NotFoundResult;

            Assert.IsNull(actionResult);
            Assert.IsNotNull(actionResult404);
        }

        [TestCase(12)]
        public void DeleteGet_IsModelValid_WithValidIncome(int? id)
        {
            mockRepo.Setup(p => p.getElement(id.Value)).Returns(getPost(id.Value));

            var result = postsController.Delete(id);
            var actionResult = result as ViewResult;

            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Model);
        }

        [TestCase(12, 12, "01.01.2000", "PostName", "1111", "SampleText")]
        public void EditPost_RedirectsToController_WithValidModel(int Id,  int postId, string Date, string postName, string rating, string text)
        {
            var result = postsController.Edit(Id, new Post { Id = postId, Date = Date, PostName = postName, Rating = rating, Text = text }).Result;
            var actionResult = result as RedirectToActionResult;

            Assert.That(actionResult.ActionName, Is.EqualTo("Index"));
        }

        [TestCase(12)]
        public void DeletePost_RedirectsToController_WithValidModel(int Id)
        {
            var result = postsController.DeleteConfirmed(Id).Result;
            var actionResult = result as RedirectToActionResult;

            Assert.That(actionResult.ActionName, Is.EqualTo("Index"));
        }

        private IEnumerable<Post> getPostsList()
        {
            var Posts = new List<Post>();
            Posts.Add(new Post()
            {
                Id = 1,
                PostName = "TestPostName",
            });
            Posts.Add(new Post()
            {
                Id = 2,
                PostName = "TestPostName2",
            });
            return Posts;
        }

        private Post getPost(int id) => new Post { Id = id, Text = "Sample Text" };
    }
}


