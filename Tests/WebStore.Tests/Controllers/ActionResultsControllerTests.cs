using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using WebStore.Controllers;
using WebStore.Domain.ViewModels;
using Xunit;

namespace WebStore.Tests.Controllers
{
    public class ActionResultsControllerTests
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ActionResultsControllerTests()
        {
            var webRootPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                @"..\..\..\..\..\UI\WebStore\wwwroot"
            );
            
            _webHostEnvironment = Substitute.For<IWebHostEnvironment>();
            _webHostEnvironment.WebRootPath.Returns(webRootPath);
        }
        
        [Fact]
        public void IndexReturnsView()
        {
            var controller = new ActionResultsController(_webHostEnvironment);

            var result = controller.Index();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void MergeContentStringReturnsContent()
        {
            const string expectedMessage = "Hi, Guest. I'm mere string content result";
            var controller = new ActionResultsController(_webHostEnvironment);

            var result = controller.MergeContentString("Guest");

            var resultData = Assert.IsType<ContentResult>(result);
            Assert.Equal(expectedMessage, resultData.Content);
        }

        [Fact]
        public void NothingReturnsEmpty()
        {
            var controller = new ActionResultsController(_webHostEnvironment);
            
            var result = controller.Nothing();
            
            Assert.IsType<EmptyResult>(result);
        }
        
        [Fact]
        public void Nothing204ReturnsNoContent()
        {
            var controller = new ActionResultsController(_webHostEnvironment);
            
            var result = controller.Nothing204();
            
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void JsonObjectSerializedReturnsJson()
        {
            var expectedEmployee = new EmployeeViewModel
            {
                Id = 1,
                FirstName = "Иван",
                SecondName = "Иванов",
                Patronymic = "Иванович",
                Age = 22
            };
            var controller = new ActionResultsController(_webHostEnvironment);
            
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
        public void GoGoogleRedirectsToGoogle()
        {
            const string expectedUrl = "https://google.com";
            var controller = new ActionResultsController(_webHostEnvironment);
            
            var result = controller.GoGoogle();
            
            var resultData = Assert.IsType<RedirectResult>(result);
            Assert.Equal(expectedUrl, resultData.Url);
        }

        [Fact]
        public void GoToHomePageRedirectsToHomePage()
        {
            var controller = new ActionResultsController(_webHostEnvironment);

            var result = controller.GoToHomePage();
            
            var resultData = Assert.IsType<LocalRedirectResult>(result);
            Assert.NotNull(resultData.Url);
            Assert.Equal("~/Home/Index", resultData.Url);
        }
        
        [Fact]
        public void RedirectWithParametersRedirectsToMergeContentString()
        {
            var controller = new ActionResultsController(_webHostEnvironment);

            var result = controller.RedirectWithParameters();
            
            var resultData = Assert.IsType<RedirectToActionResult>(result);
            Assert.NotNull(resultData.ControllerName);
            Assert.Equal(nameof(ActionResultsController.MergeContentString), resultData.ActionName);
        }
        
        [Fact]
        public void ForbiddenResourceReturnsStatusCode403()
        {
            var controller = new ActionResultsController(_webHostEnvironment);

            var result = controller.ForbiddenResource();
            
            var resultData = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(403, resultData.StatusCode);
        }
        
        [Fact]
        public void NotFoundResourceReturnsNotFound()
        {
            const string expectedMessage = "Nothing found. Sorry.";
            var controller = new ActionResultsController(_webHostEnvironment);

            var result = controller.NotFoundResource();
            
            var resultData = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, resultData.StatusCode);
            Assert.Equal(expectedMessage, resultData.Value.ToString());
        }

        [Fact]
        public void AgeCheckReturnsSorry()
        {
            const string expectedMessage = "Sorry. Adults only";
            var controller = new ActionResultsController(_webHostEnvironment);

            var result = controller.AgeCheck(1);
            
            var resultData = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal(expectedMessage, resultData.Value.ToString());
        }
        
        [Fact]
        public void AgeCheckReturnsWelcome()
        {
            const string expectedMessage = "You're welcome";
            var controller = new ActionResultsController(_webHostEnvironment);

            var result = controller.AgeCheck(18);
            
            var resultData = Assert.IsType<ContentResult>(result);
            Assert.Equal(expectedMessage, resultData.Content);
        }
        
        [Fact]
        public void TellMeItsOkReturnsOk()
        {
            const string expectedMessage = "Everything is gonna be fine!";
            var controller = new ActionResultsController(_webHostEnvironment);

            var result = controller.TellMeItsOk();
            
            var resultData = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedMessage, resultData.Value.ToString());
        }
        
        [Fact]
        public void ReallyBadRequestReturnsView()
        {
            var controller = new ActionResultsController(_webHostEnvironment);

            var result = controller.ReallyBadRequest("Not null or empty");
            
            Assert.IsType<ViewResult>(result);
        }
        
        [Fact]
        public void ReallyBadRequestReturnsBadRequest()
        {
            const string expectedMessage = "Some parameter was expected";
            var controller = new ActionResultsController(_webHostEnvironment);

            var result = controller.ReallyBadRequest(string.Empty);
            
            var resultData = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(expectedMessage, resultData.Value.ToString());
        }
        
        [Fact]
        public void GetFileReturnsPhysicalFile()
        {
            var controller = new ActionResultsController(_webHostEnvironment);

            var result = controller.GetFile();
            
            Assert.IsType<PhysicalFileResult>(result);
        }
        
        [Fact]
        public void GetBytesReturnsFileContent()
        {
            var controller = new ActionResultsController(_webHostEnvironment);

            var result = controller.GetBytes();
            
            Assert.IsType<FileContentResult>(result);
        }
        
        [Fact]
        public void GetStreamReturnsFileStream()
        {
            var controller = new ActionResultsController(_webHostEnvironment);

            var result = controller.GetStream();
            
            Assert.IsType<FileStreamResult>(result);
        }
    }
}