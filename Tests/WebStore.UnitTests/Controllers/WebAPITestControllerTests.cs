using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using WebStore.Controllers;
using WebStore.Interfaces.Api;
using Xunit;

namespace WebStore.UnitTests.Controllers
{
    public class WebAPITestControllerTests
    {
        [Fact]
        public void Index_ByDefault_ReturnsViewResultWithValues()
        {
            var expectedResult = new[] { "1", "2", "3" };
            var valueService = Substitute.For<IValueServices>();
            valueService.Get().Returns(expectedResult);
            var controller = new WebAPITestController(valueService);

            var result = controller.Index();

            var resultData = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<string>>(resultData.Model);
            Assert.Equal(expectedResult.Length, model.Count());
            
            valueService.Received().Get();
            //valueService.VerifyNoOtherCalls(); ???
        }
    }
}