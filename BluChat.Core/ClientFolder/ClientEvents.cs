using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.ClientFolder.EvenHandlers;
using BluChat.Core.Messages.MessageTypes.Authenticate;
using BluChat.Core.Messages.MessageTypes.GetChatMessages;

namespace BluChat.Core.ClientFolder
{
    public class ClientEvents(Client client)
    {
        private Client _client = client;


        public event EventHandler<UserVerifiedEventHandler> UserVerified;

        public void OnUserVerificion(ClientSuccessVerification message)
        {
            UserVerifiedEventHandler handler = new UserVerifiedEventHandler(message.authenticatedUser);
            UserVerified.Invoke(_client, handler);
        }

        public event EventHandler<GetChatMessagesEventHandler> GetChatMessages; 

        public void OnGetChatMessages(ClientMultipleStringMessages message)
        {
            GetChatMessagesEventHandler handler = new GetChatMessagesEventHandler(){Messages = message.Content};
            GetChatMessages.Invoke(_client, handler);
        }
    }
}
