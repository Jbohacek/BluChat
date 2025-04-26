using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.ServerFolder;

namespace BluChat.ServerConsole.Commands
{
    public class HelpCommand(Server server) : Command(server)
    {
        public override string Name => "help";
        public override string Description => "it helps";

        public override void InvokeCommand(string[] inputs)
        {
            var commandTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Command)) && !t.IsAbstract).ToList();

            commandTypes = commandTypes.OrderBy(x => x.Name).ToList();
            commandTypes.Remove(typeof(NotFoundCommand));

            Console.WriteLine("<--- Help --->");
            Console.WriteLine("if you type \"?\" after a command it will display his meaning");
            Console.WriteLine("Command\t\tDescription");
            Console.WriteLine("-------\t\t-----------");
            foreach (var type in commandTypes)
            {
                // Create an instance of each Command subclass
                if (Activator.CreateInstance(type, server) is Command commandInstance)
                {
                    Console.WriteLine($"{commandInstance.Name,-15}\t{commandInstance.Description}");
                }
            }
            Console.WriteLine("<--- End --->");
        }
    }
}
