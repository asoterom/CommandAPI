using System;
using Xunit;
using CommandAPI.Models;

namespace CommandAPI.Tests
{
    public class CommandTests : IDisposable
    {
        Command testCommand;

        public CommandTests()
        {
            testCommand = new Command 
            {
                HowTo = "Do somenthing",
                Platform = "Some platform",
                CommandLine = "Some commandline"
            };
            
        }
        [Fact]
        public void CanChangeHowTo()
        {
            // Arrange

            // Act

            testCommand.HowTo = "Execute Unit Tests";
        
            // Assert
            Assert.Equal("Execute Unit Tests",testCommand.HowTo);
        } 
        [Fact]
        public void CanChangePlatform()
        {
            // Arrange

            // Act
            testCommand.Platform = "vb6";
            
            // Assert
            Assert.Equal("vb6",testCommand.Platform);

        }

        [Fact]
        public void CanChangeCommandLine()
        {
            // Arrange

            // Act
            testCommand.CommandLine = "dotnet build";
            
            // Assert
            Assert.Equal("dotnet build",testCommand.CommandLine);
        }

        public void Dispose()
        {
            testCommand = null;
        }
    }
}