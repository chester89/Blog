using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using PersonalSite.Core.Contracts;
using PersonalSite.Core.Entities;
using PersonalSite.Web.Controllers;
using Xunit;

namespace PersonalSite.Web.UnitTests.Controllers
{
    public class BlogControllerTests
    {
        private Mock<IPostRepository> blogRepository;
        private BlogController controller;

        public BlogControllerTests()
        {
            blogRepository = new Mock<IPostRepository>();
            blogRepository.Setup(x => x.GetAll()).Returns(new List<Post>().AsQueryable);
            controller = new BlogController(blogRepository.Object);
        }

        [Fact]
        public void should_list_all_blog_posts()
        {
            controller.List();
            blogRepository.Verify(m => m.GetAll(), Times.Once(), "GetAll method should be called");
        }

        [Fact]
        public void can_add_post()
        {
            controller.Add();
            blogRepository.Verify(m => m.AddNew(It.IsAny<Post>()), Times.Once(), "AddNew method should be called");
        }
    }
}
