using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.Networking;

namespace BluChat.ServerConsole.Commands.ChatFolder
{
    public class ChatUpdate(Server server) : Command(server)
    {
        public override string Name => "ChatUpdate";
        public override string Description => "It will update the chat";
        public override string Format => "[chatname] [newname]";
        public override void InvokeCommand(string[] inputs)
        {
            if (!CheckFormat(inputs)) return;

            var chat = server.Database.Chats.GetFirstOrDefault(x => x.Name.ToLower() == inputs[1].ToLower());
            if (chat == null)
            {
                Commander.SendErrorMessage("no chat found");
                return;
            }

            chat.Name = inputs[2];

            server.Database.Chats.Update(chat);
            server.Database.Save();

            Commander.SendSuccessMessage("Chat name changed succesfuly");

        }
    }
}
