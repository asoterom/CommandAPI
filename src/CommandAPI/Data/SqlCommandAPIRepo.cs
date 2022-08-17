using CommandAPI.Data;
using CommandAPI.Models;
using System.Linq;
using System.Collections.Generic;

namespace CommandAPI.Data
{
    public class SQLCommandAPIRepo :ICommandAPIRepo
    {
        private readonly CommandContext _context;

        public SQLCommandAPIRepo(CommandContext context)
        {
            _context = context;
        }

        public void CreateCommand(Command cmd)
        {
            if (cmd == null)
            {
                throw new NotImplementedException();
            }
            _context.CommandItems.Add(cmd);
        }

        public void DeleteCommand(Command cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException();
            }

            _context.CommandItems.Remove(cmd);
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return _context.CommandItems.ToList();
        }

        public Command GetCommandById(int id)
        {
            return _context.CommandItems.FirstOrDefault(c => c.id == id) ;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0) ;
        }

        public void UpdateCommand(Command cmd)
        {
            
        }
    }
}