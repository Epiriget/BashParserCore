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
    public class CommentsControllerTests
    {
        [TestCase(0, 1, "SampleText")]
        public void Create_RedirectsToPostDetails(int parentId, int postId, string text)
        {
            Mock<ApplicationDbContext> mockContext = new Mock<ApplicationDbContext>();
            Mock<ICurrentUserService> mockUserService = new Mock<ICurrentUserService>();
            mockUserService.Setup(s => s.getCurrentUser()).Returns(new ApplicationUser { Id = "1234", UserName = "TestUser" });
            CommentsController commentController = new CommentsController(mockContext.Object, mockUserService.Object);

            IActionResult result = commentController.Create(new Comment { ParentID = parentId, PostId = postId, Text = text}).Result;
            var actionResult = result as RedirectResult;

            Assert.That(actionResult.Url.Contains($"Posts/Details/{postId}"));
        }
    }
}
