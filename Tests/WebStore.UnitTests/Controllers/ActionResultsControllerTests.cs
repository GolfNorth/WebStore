using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using WebStore.Controllers;
using WebStore.Domain.ViewModels;
using Xunit;

namespace WebStore.UnitTests.Controllers
{
    public class ActionResultsControllerTests
    {
        private static ActionResultsController MakeActionResultsController()
        {
            var webRootPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                @"..\..\..\..\..\UI\WebStore\wwwroot"
            );
            
            var webHostEnvironment = Substitute.For<IWebHostEnvironment>();
            webHostEnvironment.WebRootPath.Returns(webRootPath);
            
            return new ActionResultsController(webHostEnvironment);
        }
        
        [Fact]
        public void Index_ByDefault_ReturnsViewResult()
        {
            var controller = MakeActionResultsController();

            var result = controller.Index();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void MergeContentString_ByDefaultReturnsContentResultWithMessage()
        {
            var controller = MakeActionResultsController();
            const string expectedMessage = "Hi, Guest. I'm mere string content result";

            var result = controller.MergeContentString("Guest");

            var resultData = Assert.IsType<ContentResult>(result);
            Assert.Equal(expectedMessage, resultData.Content);
        }

        [Fact]
        public void Nothing_ByDefault_ReturnsEmptyResult()
        {
            var controller = MakeActionResultsController();
            
            var result = controller.Nothing();
            
            Assert.IsType<EmptyResult>(result);
        }
        
        [Fact]
        public void Nothing204_ByDefault_ReturnsNoContentResult()
        {
            var controller = MakeActionResultsController();
            
            var result = controller.Nothing204();
            
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void JsonObjectSerialized_ByDefault_ReturnsJsonResultWithEmployeeViewModel()
        {
            var controller = MakeActionResultsController();
            var expectedEmployee = new EmployeeViewModel
            {
                Id = 1,
                FirstName = "Иван",
                SecondName = "Иванов",
                Patronymic = "Иванович",
                Age = 22
            };
            
            var result = controller.JsonObjectSerialized();
            
            var resultData = Assert.IsType<JsonResult>(result);
            dynamic jsonData = new JsonResultDynamicWrapper(resultData);
            Assert.Equal(expectedEmployee.Id, jsonData.Id);
            Assert.Equal(expectedEmployee.FirstName, jsonData.FirstName);
            Assert.Equal(expectedEmployee.SecondName, jsonData.SecondName);
            Assert.Equal(expectedEmployee.Patronymic, jsonData.Patronymic);
            Assert.Equal(expectedEmployee.Age, jsonData.Age);
        }

        [Fact]
        public void GoGoogle_ByDefault_RedirectsToGoogle()
        {
            var controller = MakeActionResultsController();
            const string expectedUrl = "https://google.com";
            
            var result = controller.GoGoogle();
            
            var resultData = Assert.IsType<RedirectResult>(result);
            Assert.Equal(expectedUrl, resultData.Url);
        }

        [Fact]
        public void GoToHomePage_ByDefault_RedirectsToHomePage()
        {
            var controller = MakeActionResultsController();

            var result = controller.GoToHomePage();
            
            var resultData = Assert.IsType<LocalRedirectResult>(result);
            Assert.NotNull(resultData.Url);
            Assert.Equal("~/Home/Index", resultData.Url);
        }
        
        [Fact]
        public void RedirectWithParameters_ByDefault_RedirectsToMergeContentString()
        {
            var controller = MakeActionResultsController();

            var result = controller.RedirectWithParameters();
            
            var resultData = Assert.IsType<RedirectToActionResult>(result);
            Assert.NotNull(resultData.ControllerName);
            Assert.Equal(nameof(ActionResultsController.MergeContentString), resultData.ActionName);
        }
        
        [Fact]
        public void ForbiddenResource_ByDefault_ReturnsStatusCodeResultWithCode403()
        {
            var controller = MakeActionResultsController();

            var result = controller.ForbiddenResource();
            
            var resultData = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(403, resultData.StatusCode);
        }
        
        [Fact]
        public void NotFoundResource_ByDefault_ReturnsNotFoundResult()
        {
            var controller = MakeActionResultsController();
            const string expectedMessage = "Nothing found. Sorry.";

            var result = controller.NotFoundResource();
            
            var resultData = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, resultData.StatusCode);
            Assert.Equal(expectedMessage, resultData.Value.ToString());
        }

        [Fact]
        public void AgeCheck_WhenAgeLessThan18_ReturnsUnauthorizedObjectResultWithMessage()
        {
            var controller = MakeActionResultsController();
            const string expectedMessage = "Sorry. Adults only";

            var result = controller.AgeCheck(1);
            
            var resultData = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal(expectedMessage, resultData.Value.ToString());
        }
        
        [Fact]
        public void AgeCheck_WhenAgeMoreThan_ReturnsContentResultWithMessage()
        {
            var controller = MakeActionResultsController();
            const string expectedMessage = "You're welcome";

            var result = controller.AgeCheck(18);
            
            var resultData = Assert.IsType<ContentResult>(result);
            Assert.Equal(expectedMessage, resultData.Content);
        }
        
        [Fact]
        public void TellMeItsOk_ByDefault_ReturnsOkObjectResultWithMessage()
        {
            var controller = MakeActionResultsController();
            const string expectedMessage = "Everything is gonna be fine!";

            var result = controller.TellMeItsOk();
            
            var resultData = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedMessage, resultData.Value.ToString());
        }
        
        [Fact]
        public void ReallyBadRequest_ByDefault_ReturnsViewResult()
        {
            var controller = MakeActionResultsController();

            var result = controller.ReallyBadRequest("Not null or empty");
            
            Assert.IsType<ViewResult>(result);
        }
        
        [Fact]
        public void ReallyBadRequest_ByDefault_ReturnsBadRequestObjectResultWithMessage()
        {
            var controller = MakeActionResultsController();
            const string expectedMessage = "Some parameter was expected";

            var result = controller.ReallyBadRequest(string.Empty);
            
            var resultData = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(expectedMessage, resultData.Value.ToString());
        }
        
        [Fact]
        public void GetFile_ByDefault_ReturnsPhysicalFileResult()
        {
            var controller = MakeActionResultsController();

            var result = controller.GetFile();
            
            Assert.IsType<PhysicalFileResult>(result);
        }
        
        [Fact]
        public void GetBytes_ByDefault_ReturnsFileContentResult()
        {
            var controller = MakeActionResultsController();

            var result = controller.GetBytes();
            
            Assert.IsType<FileContentResult>(result);
        }
        
        [Fact]
        public void GetStream_ByDefault_ReturnsFileStreamResult()
        {
            var controller = MakeActionResultsController();

            var result = controller.GetStream();
            
            Assert.IsType<FileStreamResult>(result);
        }
    }
}