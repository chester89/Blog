using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using PersonalSite.Core.Contracts;
using Xunit;

namespace PersonalSite.Web.UnitTests.Controllers
{
    public class BlogControllerTests
    {
        private Mock<IBlogRepository> blogRepository;
        private BlogController controller;

        public BlogControllerTests()
        {
            blogRepository = new Mock<IBlogRepository>();
            controller = new BlogController(blogRepository);
        }

        [Fact]
        public void should_list_all_blog_posts()
        {
            
        }
    }
}
