using CommandAPI.Controllers;
using CommandAPI.Data;
using CommandAPI.Models;
using CommandAPI.Profiles;
using Moq;
using AutoMapper;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using CommandAPI.Dtos;

namespace CommandAPI.Tests
{
    public class CommandsControllerTests :IDisposable
    {
        Mock<ICommandAPIRepo> _mockRepo;
        CommandsProfile _realProfile;
        MapperConfiguration _configuration;
        IMapper _mapper;

        public CommandsControllerTests()
        {
            _mockRepo = new Mock<ICommandAPIRepo>();
            _realProfile = new CommandsProfile();
            _configuration = new MapperConfiguration( cfg => cfg.AddProfile(_realProfile));
            _mapper = new Mapper(_configuration);
            
        }
        public void Dispose()
        {
            _mockRepo = null;
            _realProfile = null;
            _configuration = null;
            _mapper = null;

        }

        [Fact]
        public void GetCommandItems_ReturnZeroitems_WhenDBIsEmpty()  
        {
            // Arrange

            _mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(0));

            // creamos una instancia de la clase commandcontroller /*Reporsitorio, Automapper*/
            var controller = new CommandsController(_mockRepo.Object,_mapper);

        
            // Act
        
            var result = controller.GetAllCommands();
            
            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        private List<Command> GetCommands(int num)
        {
            var commands = new List<Command>();
            if (num > 0)
            {
                commands.Add(
                    new Command{
                        id = 0,
                        HowTo = "How to generate a migration",
                        CommandLine = "dotnet ef migrations add <Name of migration>",
                        Platform = ".Net core EF"
                    });
            }
            return commands;
        }
 
        [Fact]
        public void GetAllCommand_ReturnOneItem_WhenDBHasOneResource()
        {
            // Arrange

            _mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));

            // creamos una instancia de la clase commandcontroller /*Reporsitorio, Automapper*/
            var controller = new CommandsController(_mockRepo.Object,_mapper);

        
            // Act
        
            var result = controller.GetAllCommands();
            
            // Assert
            var okResult = result.Result as OkObjectResult;
            var commands = okResult.Value as List<CommandReadDto>;
            Assert.Single(commands);


        }
        [Fact]
        public void GetAllCommand_Return200Ok_WhenDBHasOneResource()
        {
            // Arrange

            _mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));

            // creamos una instancia de la clase commandcontroller /*Reporsitorio, Automapper*/
            var controller = new CommandsController(_mockRepo.Object,_mapper);

        
            // Act
        
            var result = controller.GetAllCommands();
            
            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
       }

       [Fact]
       public void GetAllCommands_ReturnsCorrectType_WhenDBHasOneResource()
       {
            // Arrange

            _mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));

            // creamos una instancia de la clase commandcontroller /*Reporsitorio, Automapper*/
            var controller = new CommandsController(_mockRepo.Object,_mapper);

        
            // Act
        
            var result = controller.GetAllCommands();
            
            // Assert
            Assert.IsType<ActionResult<IEnumerable<CommandReadDto>>>(result);
       }

       [Fact]
       public void GetCommandByID_Returns404NotFound_WhenNonExistentIDProvided()
       {
            // Arrange

            _mockRepo.Setup(repo => repo.GetCommandById(0)).Returns(()=> null);


            // creamos una instancia de la clase commandcontroller /*Reporsitorio, Automapper*/
            var controller = new CommandsController(_mockRepo.Object,_mapper);

        
            // Act
        
            var result = controller.GetCommandById(1);
            
            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
       }
 
        [Fact]
        public void GetCommandByID_Returns200Ok_WhenValidIDProvided()
        {
            // Arrange

            _mockRepo.Setup(repo => repo.GetCommandById(1)).Returns( new Command {
                id =1,
                HowTo = "mock",
                Platform = "Mock",
                CommandLine = "Mock"
            });


            // creamos una instancia de la clase commandcontroller /*Reporsitorio, Automapper*/
            var controller = new CommandsController(_mockRepo.Object,_mapper);

        
            // Act
        
            var result = controller.GetCommandById(1);
            
            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
        [Fact]
        public void GetCommandByID_ReturnsCorrectType_WhenValidIDProvided()
        {
            // Arrange

            _mockRepo.Setup(repo => repo.GetCommandById(1)).Returns( new Command {
                id =1,
                HowTo = "mock",
                Platform = "Mock",
                CommandLine = "Mock"
            });


            // creamos una instancia de la clase commandcontroller /*Reporsitorio, Automapper*/
            var controller = new CommandsController(_mockRepo.Object,_mapper);

        
            // Act
        
            var result = controller.GetCommandById(1);
            
            // Assert
            Assert.IsType<ActionResult<CommandReadDto>>(result);
        }
        [Fact]
        public void CreatedCommand_ReturnsCorrectResourceType_WhenValidObjectSubmitted()
        {
            // Arrange

            _mockRepo.Setup(repo => repo.GetCommandById(1)).Returns( new Command {
                id =1,
                HowTo = "mock",
                Platform = "Mock",
                CommandLine = "Mock"
            });


            // creamos una instancia de la clase commandcontroller /*Reporsitorio, Automapper*/
            var controller = new CommandsController(_mockRepo.Object,_mapper);

        
            // Act
        
            var result = controller.CreateCommand(new CommandCreateDto{});
            
            // Assert
            Assert.IsType<ActionResult<CommandReadDto>>(result);
        }

        [Fact]
        public void CreatedCommand_Returns201Created_WhenValidObjectSubmitted()
        {
            // Arrange

            _mockRepo.Setup(repo => repo.GetCommandById(1)).Returns( new Command {
                id =1,
                HowTo = "mock",
                Platform = "Mock",
                CommandLine = "Mock"
            });


            // creamos una instancia de la clase commandcontroller /*Reporsitorio, Automapper*/
            var controller = new CommandsController(_mockRepo.Object,_mapper);

        
            // Actcd..
        
            var result = controller.CreateCommand(new CommandCreateDto{});
            
            // Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }
        [Fact]
        public void UpdateCommand_Return204NoContent_WhenValidObjectSubmitted()
        {
            // Arrange

            _mockRepo.Setup(repo => repo.GetCommandById(1)).Returns( new Command {
                id =1,
                HowTo = "mock",
                Platform = "Mock",
                CommandLine = "Mock"
            });


            // creamos una instancia de la clase commandcontroller /*Reporsitorio, Automapper*/
            var controller = new CommandsController(_mockRepo.Object,_mapper);

        
            // Act
        
            var result = controller.UpdateCommand(1,new CommandUpdateDto{});
            
            // Assert
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public void UpdateCommand_Return404NoFound_WhenNonExistentResourceIDSubmitted()
        {
            // Arrange

            _mockRepo.Setup(repo => repo.GetCommandById(0)).Returns(()=> null );


            // creamos una instancia de la clase commandcontroller /*Reporsitorio, Automapper*/
            var controller = new CommandsController(_mockRepo.Object,_mapper);

        
            // Act
        
            var result = controller.UpdateCommand(1,new CommandUpdateDto{});
            
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void PartialCommandUpdate_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            // Arrange

            _mockRepo.Setup(repo => repo.GetCommandById(0)).Returns(()=> null );


            // creamos una instancia de la clase commandcontroller /*Reporsitorio, Automapper*/
            var controller = new CommandsController(_mockRepo.Object,_mapper);

        
            // Act
        
            var result = controller.PartialCommandUpdate(0, new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<CommandUpdateDto>{});
            
            // Assert
            Assert.IsType<NotFoundResult>(result);
         }
         [Fact]
         public void DeleteCommand_Returns204noContent_WhenValidresourceIDSubmitted()
         {
             // Arrange

            _mockRepo.Setup(repo => repo.GetCommandById(1)).Returns( new Command {
                id =1,
                HowTo = "mock",
                Platform = "Mock",
                CommandLine = "Mock"
            });


            // creamos una instancia de la clase commandcontroller /*Reporsitorio, Automapper*/
            var controller = new CommandsController(_mockRepo.Object,_mapper);

        
            // Act
        
            var result = controller.DeleteCommand(1);
            
            // Assert
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public void DeleteCommand_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            // Arrange

            _mockRepo.Setup(repo => repo.GetCommandById(0)).Returns(()=> null );


            // creamos una instancia de la clase commandcontroller /*Reporsitorio, Automapper*/
            var controller = new CommandsController(_mockRepo.Object,_mapper);

        
            // Act
        
            var result = controller.DeleteCommand(0);
            
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}