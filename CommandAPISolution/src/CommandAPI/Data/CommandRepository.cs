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
            int entityCount = _context.SaveChanges();

            return entityCount >= 0;
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
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            _context.CommandItems.Add(command);
        }

        public void UpdateCommand(Command command)
        {
            // No Implementation (EF will track entity updates for us)
        }

        public void DeleteCommand(Command command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            _context.CommandItems.Remove(command);
        }
    }
}
