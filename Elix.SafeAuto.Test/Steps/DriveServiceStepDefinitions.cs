using Elix.SafeAuto.Application;
using FluentAssertions;
using System;
using TechTalk.SpecFlow;

namespace Elix.SafeAuto.Test.Steps
{
    [Binding, Scope(Tag = "Service")]
    public sealed class DriveServiceStepDefinitions
    {

        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;
        private readonly IDriverService _service;

        public DriveServiceStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _service = new DriverService();
        }

        [Given("we have a sample command list")]
        public void WeHaveASampleCommandList(string contents)
        {
            _scenarioContext.Add("commands", contents);
        }

        [When("the file is processed")]
        public void WhenTheFileIsProcessed()
        {
            string commands = _scenarioContext["commands"] as string;

            try
            {
                _scenarioContext.Add("result", _service.Process(commands));
            }
            catch (Exception exc)
            {
                _scenarioContext.Add("result", exc);
            }
        }

        [Then("the expected result is")]
        public void ThenTheExpectedResultIs(string result)
        {
            _scenarioContext["result"].Should().BeOfType(typeof(string));
            string output = _scenarioContext["result"] as string;
            output.Should().Be(result);
        }

        [Then("the expected result is an error with message")]
        public void ThenTheExpectedResultIsAnError(string message)
        {
            _scenarioContext["result"].Should().BeAssignableTo(typeof(Exception));
            Exception output = _scenarioContext["result"] as Exception;
            output.Message.Should().Be(message);
        }
    }
}