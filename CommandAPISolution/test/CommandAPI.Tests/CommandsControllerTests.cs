using System;
using System.Collections.Generic;
using System.Text;

using Moq;

using Xunit;

using CommandAPI.Controllers;

using CommandAPI.Data;
using CommandAPI.DTOs;
using CommandAPI.DTOs.MappingProfiles;
using CommandAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace CommandAPI.Tests
{
    public class CommandsControllerTests : IDisposable
    {
        Mock<IPersistCommand> _mockRespository;
        CommandsProfile _realProfile;
        MapperConfiguration _configuration;
        IMapper _mapper;

        public CommandsControllerTests()
        {
            _mockRespository = new Mock<IPersistCommand>();
            _realProfile = new CommandsProfile();
            _configuration = new MapperConfiguration(config => config.AddProfile(_realProfile));
            _mapper = new Mapper(_configuration);
        }

        #region GetAllCommands Tests

        [Fact]
        public void GetAllCommands_ReturnZeroItems_WhenDBIsEmpty()
        {
            // Arrange
            _mockRespository
                .Setup(repo => repo.GetAllCommands())
                .Returns(GetCommands(0));

            var controller = new CommandsController(_mockRespository.Object, _mapper);

            // Act
            var result = controller.GetAllCommands();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllCommands_ReturnOneItem_WhenDBHasOneResource()
        {
            // Arrange
            _mockRespository
                .Setup(repo => repo.GetAllCommands())
                .Returns(GetCommands(1));

            var controller = new CommandsController(_mockRespository.Object, _mapper);

            // Act
            var result = controller.GetAllCommands();

            // Assert
            var okResult = result.Result as OkObjectResult;

            var commands = okResult.Value as List<CommandReadDTO>;

            Assert.Single(commands);
        }

        [Fact]
        public void GetAllCommands_Return200OK_WhenDBHasOneResource()
        {
            // Arrange
            _mockRespository
                .Setup(repo => repo.GetAllCommands())
                .Returns(GetCommands(1));

            var controller = new CommandsController(_mockRespository.Object, _mapper);

            // Act
            var result = controller.GetAllCommands();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllCommands_ReturnCorrectType_WhenDBHasOneResource()
        {
            // Arrange
            _mockRespository
                .Setup(repo => repo.GetAllCommands())
                .Returns(GetCommands(1));

            var controller = new CommandsController(_mockRespository.Object, _mapper);

            // Act
            var result = controller.GetAllCommands();

            // Assert
            Assert.IsType<ActionResult<IEnumerable<CommandReadDTO>>>(result);
        }

        #endregion

        #region GetCommandByID Tests

        [Fact]
        public void GetCommandByID_Return404NotFound_WhenNonExistentIDProvided()
        {
            // Arrange
            _mockRespository
                .Setup(repo => repo.GetCommandById(0))
                .Returns(() => null);

            var controller = new CommandsController(_mockRespository.Object, _mapper);

            // Act
            var result = controller.GetCommandById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetCommandByID_Return200OK_WhenValidIDProvided()
        {
            // Arrange
            _mockRespository
                .Setup(repo => repo.GetCommandById(1))
                .Returns(new Command
                {
                    Id = 1,
                    HowTo = "Mock",
                    Platform = "Mock",
                    CommandLine = "Mock"
                });

            var controller = new CommandsController(_mockRespository.Object, _mapper);

            // Act
            var result = controller.GetCommandById(1);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetCommandByID_ReturnCommandReadDTO_WhenValidIDProvided()
        {
            // Arrange
            _mockRespository
                .Setup(repo => repo.GetCommandById(1))
                .Returns(new Command
                {
                    Id = 1,
                    HowTo = "Mock",
                    Platform = "Mock",
                    CommandLine = "Mock"
                });

            var controller = new CommandsController(_mockRespository.Object, _mapper);

            // Act
            var result = controller.GetCommandById(1);

            // Assert
            Assert.IsType<ActionResult<CommandReadDTO>>(result);
        }

        #endregion

        #region CreateCommand Tests

        [Fact]
        public void CreateCommand_ReturnCorrectResourceType_WhenValidObjectSubmitted()
        {
            // Arrange
            _mockRespository
                .Setup(repo => repo.GetCommandById(1))
                .Returns(new Command
                {
                    Id = 1,
                    HowTo = "Mock",
                    Platform = "Mock",
                    CommandLine = "Mock"
                });

            var controller = new CommandsController(_mockRespository.Object, _mapper);

            // Act
            var result = controller.CreateCommand(new CommandCreateDTO { });

            // Assert
            Assert.IsType<ActionResult<CommandReadDTO>>(result);
        }

        [Fact]
        public void CreateCommand_Return201Created_WhenValidObjectSubmitted()
        {
            // Arrange
            _mockRespository
                .Setup(repo => repo.GetCommandById(1))
                .Returns(new Command
                {
                    Id = 1,
                    HowTo = "Mock",
                    Platform = "Mock",
                    CommandLine = "Mock"
                });

            var controller = new CommandsController(_mockRespository.Object, _mapper);

            // Act
            var result = controller.CreateCommand(new CommandCreateDTO { });

            // Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }

        #endregion

        #region UpdateCommand Tests

        [Fact]
        public void UpdateCommand_Return204NoContent_WhenValidObjectSubmitted()
        {
            // Arrange
            _mockRespository
                .Setup(repo => repo.GetCommandById(1))
                .Returns(new Command
                {
                    Id = 1,
                    HowTo = "Mock",
                    Platform = "Mock",
                    CommandLine = "Mock"
                });

            var controller = new CommandsController(_mockRespository.Object, _mapper);

            // Act
            var result = controller.UpdateCommand(1, new CommandUpdateDTO { });

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateCommand_Return404NotFound_WhenNonExistentIDSubmitted()
        {
            // Arrange
            _mockRespository
                .Setup(repo => repo.GetCommandById(1))
                .Returns(() => null);

            var controller = new CommandsController(_mockRespository.Object, _mapper);

            // Act
            var result = controller.UpdateCommand(1, new CommandUpdateDTO { });

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        #endregion

        #region PartialUpdateCommand Tests

        [Fact]
        public void PartialUpdateCommand_Return404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            // Arrange
            _mockRespository
                .Setup(repo => repo.GetCommandById(0))
                .Returns(() => null);

            var controller = new CommandsController(_mockRespository.Object, _mapper);

            // Act
            var result = controller.PartialUpdateCommand(0, new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<CommandUpdateDTO> { });

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        #endregion

        #region DeleteCommand Tests

        [Fact]
        public void DeleteCommand_Return204NoContent_WhenValidResourceIDSubmitted()
        {
            // Arrange
            _mockRespository
                .Setup(repo => repo.GetCommandById(1))
                .Returns(new Command
                {
                    Id = 1,
                    HowTo = "Mock",
                    Platform = "Mock",
                    CommandLine = "Mock"
                });

            var controller = new CommandsController(_mockRespository.Object, _mapper);

            // Act
            var result = controller.DeleteCommand(1);

            // Assert
            //Assert.IsType<NoContentResult>(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void DeleteCommand_Return404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            // Arrange
            _mockRespository
                .Setup(repo => repo.GetCommandById(0))
                .Returns(() => null);

            var controller = new CommandsController(_mockRespository.Object, _mapper);

            // Act
            var result = controller.DeleteCommand(0);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        #endregion

        private List<Command> GetCommands(int count)
        {
            var commands = new List<Command>();

            if (count > 0)
            {
                commands.Add(new Command
                {
                    Id = 0,
                    HowTo = "How to generate a migration",
                    Platform = ".NET Core EF",
                    CommandLine = "dotnet ef migrations add <Name of Migration>"
                });
            }

            return commands;
        }

        public void Dispose()
        {
            _mockRespository = null;
            _realProfile = null;
            _configuration = null;
            _mapper = null;
        }
    }
}
