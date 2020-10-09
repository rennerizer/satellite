using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

using AutoMapper;

using CommandAPI.Data;
using CommandAPI.DTOs;
using CommandAPI.Models;

namespace CommandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly IPersistCommand _repository;
        private readonly IMapper _mapper;

        public CommandsController(IPersistCommand repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDTO>> GetAllCommands()
        {
            var commands = _repository.GetAllCommands();

            return Ok(_mapper.Map<IEnumerable<CommandReadDTO>>(commands));
        }

        [HttpGet("{id}", Name ="GetCommandById")]
        public ActionResult<CommandReadDTO> GetCommandById(int id)
        {
            var command = _repository.GetCommandById(id);

            if (command == null)
                return NotFound();

            return Ok(_mapper.Map<CommandReadDTO>(command));
        }

        [HttpPost]
        public ActionResult<CommandReadDTO> CreateCommand(CommandCreateDTO newCommand)
        {
            var commandModel = _mapper.Map<Command>(newCommand);

            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();

            var command = _mapper.Map<CommandReadDTO>(commandModel);

            return CreatedAtRoute(nameof(GetCommandById), new { Id = command.Id }, command);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDTO updatedCommand)
        {
            var commandModel = _repository.GetCommandById(id);

            if (commandModel == null)
                return NotFound();

            _mapper.Map(updatedCommand, commandModel);

            _repository.UpdateCommand(commandModel);

            _repository.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialUpdateCommand(int id, JsonPatchDocument<CommandUpdateDTO> patchedCommand)
        {
            var commandModel = _repository.GetCommandById(id);

            if (commandModel == null)
                return NotFound();

            var commandToPatch = _mapper.Map<CommandUpdateDTO>(commandModel);
            patchedCommand.ApplyTo(commandToPatch, ModelState);

            if (!TryValidateModel(commandToPatch))
                return ValidationProblem(ModelState);

            _mapper.Map(commandToPatch, commandModel);

            _repository.UpdateCommand(commandModel);

            _repository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModel = _repository.GetCommandById(id);

            if (commandModel == null)
                return NotFound();

            _repository.DeleteCommand(commandModel);

            _repository.SaveChanges();

            return NoContent();
        }
    }
}
