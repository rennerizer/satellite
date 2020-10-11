using System;
using System.Collections.Generic;
using System.Text;

using Xunit;

using CommandAPI.Models;

namespace CommandAPI.Tests
{
    public class CommandTests : IDisposable
    {
        private Command _testCommand;

        public CommandTests()
        {
            // Arrange
            _testCommand = new Command
            {
                HowTo = "Do something awesome",
                Platform = "xUnit",
                CommandLine = "dotnet test"
            };
        }

        [Fact]
        public void CanChangeHowTo()
        {
            // Act
            _testCommand.HowTo = "Execute Unit Tests";

            // Assert
            Assert.Equal("Execute Unit Tests", _testCommand.HowTo);
        }

        [Fact]
        public void CanChangePlatform()
        {
            // Act
            _testCommand.Platform = "New Platform";

            // Assert
            Assert.Equal("New Platform", _testCommand.Platform);
        }

        [Fact]
        public void CanChangeCommandLine()
        {
            // Act
            _testCommand.CommandLine = "dotnet blah blah";

            // Assert
            Assert.Equal("dotnet blah blah", _testCommand.CommandLine);
        }

        public void Dispose()
        {
            _testCommand = null;
        }
    }
}
