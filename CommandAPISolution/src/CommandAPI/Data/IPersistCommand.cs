using System.Collections.Generic;

using CommandAPI.Models;

namespace CommandAPI.Data
{
    public interface IPersistCommand
    {
        bool SaveChanges();

        IEnumerable<Command> GetAllCommands();

        Command GetCommandById(int id);

        void CreateCommand(Command command);

        void UpdateCommand(Command command);

        void DeleteCommand(Command command);
    }
}
