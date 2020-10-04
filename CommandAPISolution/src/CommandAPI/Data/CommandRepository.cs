using System;
using System.Collections.Generic;
using System.Linq;

using CommandAPI.Models;

namespace CommandAPI.Data
{
    public class CommandRepository : IPersistCommand
    {
        private readonly CommandContext _context;

        public CommandRepository(CommandContext context)
        {
            _context = context;
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = _context
                .CommandItems
                .ToList();

            return commands;
        }

        public Command GetCommandById(int id)
        {
            var command = _context
                .CommandItems
                .FirstOrDefault(item => item.Id == id);

            return command;
        }

        public void CreateCommand(Command command)
        {
            throw new NotImplementedException();
        }

        public void UpdateCommand(Command command)
        {
            throw new NotImplementedException();
        }

        public void DeleteCommand(Command command)
        {
            throw new NotImplementedException();
        }
    }
}
