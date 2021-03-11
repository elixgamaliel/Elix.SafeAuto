using Elix.SafeAuto.Api.Controllers;
using Elix.SafeAuto.Application;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.IO;
using TechTalk.SpecFlow;

namespace Elix.SafeAuto.Test.Steps
{
    [Binding, Scope(Tag = "API")]
    public sealed class DriveAPIStepDefinitions
    {

        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;
        private readonly DriveController _controller;

        public DriveAPIStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _controller = new DriveController(null, new DriverService());
        }

        [Given("we have a sample file with contents")]
        public void WeHaveASampleFile(string contents)
        {
            var fileMock = new Mock<IFormFile>();            
            var fileName = "test.txt";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(contents);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            _scenarioContext.Add("testFile", fileMock);
        }

        [When("the file is uploaded")]
        public void WhenTheFileIsUploaded()
        {
            Mock<IFormFile> file = _scenarioContext["testFile"] as Mock<IFormFile>;

            _scenarioContext.Add("result", _controller.Post(file.Object));
        }

        [Then("the expected output is")]
        public void ThenTheExpectedOutputIs(string result)
        {
            IActionResult actionResult = _scenarioContext["result"] as IActionResult;
            actionResult.Should().BeOfType(typeof(OkObjectResult));
            OkObjectResult ok = actionResult as OkObjectResult;
            ok.StatusCode.Should().HaveValue();
            ok.StatusCode.Value.Should().Be(200);
            ok.Value.Should().BeOfType(typeof(string));
            ok.Value.Should().Be(result);
        }

        [Then("the expected result is an error")]
        public void ThenTheExpectedResultIsAnError()
        {
            IActionResult actionResult = _scenarioContext["result"] as IActionResult;
            actionResult.Should().BeOfType(typeof(ObjectResult));
            ObjectResult notOk = actionResult as ObjectResult;            
            notOk.StatusCode.Should().Be(500);
        }
    }
}