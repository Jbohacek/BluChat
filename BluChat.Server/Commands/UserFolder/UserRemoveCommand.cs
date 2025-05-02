using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.Networking;

namespace BluChat.ServerConsole.Commands.UserFolder
{
    public class UserRemoveCommand(Server server) : Command(server)
    {
        public override string Name => "UserRemove";
        public override string Description => "this will remove the user";
        public override string Format => "[Username]";

        public override void InvokeCommand(string[] inputs)
        {
            if (!CheckFormat(inputs)) return;

            Core.Common.Entities.User? deleteUser = server.Database.Users.GetAll().FirstOrDefault(x => x.UserName.ToLower() == inputs[1].ToLower());
            if (deleteUser == null)
            {
                Commander.SendErrorMessage($"User {inputs[1]} not found");
                return;
            }

            if (!Commander.AskYesNo($"Are you sure to delete user {deleteUser.UserName}?"))
                return;

            server.Database.Users.Remove(deleteUser);
            server.Database.Save();

            //todo: pokud je uživatel smazán je smazáný všechny jeho zprávy > později přenout na "[deleted]" usera

            Commander.SendSuccessMessage($"{deleteUser.UserName} has been deleted");
        }
    }
}
