using Microsoft.AspNetCore.Mvc;
using WebStore.Controllers;
using Xunit;

namespace WebStore.UnitTests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_ByDefault_ReturnsView()
        {
            var controller = new HomeController();

            var result = controller.Index();

            Assert.IsType<ViewResult>(result);
        }
        
        [Fact]
        public void ContactUs_ByDefault_ReturnsView()
        {
            var controller = new HomeController();

            var result = controller.ContactUs();

            Assert.IsType<ViewResult>(result);
        }  
    }
}