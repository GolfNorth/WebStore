using Microsoft.AspNetCore.Mvc;
using WebStore.Controllers;
using Xunit;

namespace WebStore.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexReturnsView()
        {
            var controller = new HomeController();

            var result = controller.Index();

            Assert.IsType<ViewResult>(result);
        }
        
        [Fact]
        public void ContactUsReturnsView()
        {
            var controller = new HomeController();

            var result = controller.ContactUs();

            Assert.IsType<ViewResult>(result);
        }  
    }
}