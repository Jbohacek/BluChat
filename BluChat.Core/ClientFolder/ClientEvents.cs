using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.ClientFolder.EvenHandlers;
using BluChat.Core.Messages.MessageTypes.Authenticate;

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
    }
}
