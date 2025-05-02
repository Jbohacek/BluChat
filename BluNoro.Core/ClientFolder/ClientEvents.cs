using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.ClientFolder.EvenHandlers;
using BluNoro.Core.Messages.MessageTypes.Authenticate;
using BluNoro.Core.Messages.MessageTypes.GetChatMessages;
using BluNoro.Core.Messages.MessageTypes.GetChats;
using BluNoro.Core.Messages.MessageTypes.SendMessage;
using BluNoro.Core.Messages.MessageTypes.SendMessage.Confirmation;
using BluNoro.Core.Networking;

namespace BluNoro.Core.ClientFolder
{
    public class ClientEvents(Client client)
    {
        private Client _client = client;

        /// <summary>
        /// User verificaion
        /// </summary>
        public event EventHandler<UserVerifiedEventHandler> UserVerified;
        public void OnUserVerificion(ClientSuccessVerification message)
        {
            UserVerifiedEventHandler handler = new UserVerifiedEventHandler(message.authenticatedUser);
            UserVerified.Invoke(_client, handler);
        }

        /// <summary>
        /// Get chats and messages
        /// </summary>
        public event EventHandler<GetChatMessagesEventHandler> GetChatMessages; 
        public void OnGetChatMessages(ClientMultipleStringMessages message)
        {
            GetChatMessagesEventHandler handler = new GetChatMessagesEventHandler(){Messages = message.Content};
            GetChatMessages.Invoke(_client, handler);
        }

        /// <summary>
        /// User failed verification
        /// </summary>
        public event EventHandler<UserFailedVerificationHandler> UserFailedVerification;
        public void OnUserFailedVerification(ClientFailedVerification message)
        {
            UserFailedVerificationHandler handler = new(message.Reason);
            UserFailedVerification.Invoke(_client, handler);

        }

        /// <summary>
        /// If send message was successfuly verified that it is send
        /// </summary>
        public event EventHandler<SendMessageSuccessArgs> SendMessageSuccess;
        public void OnSuccesMessageSend(ClientSuccessSendMessage message)
        {
            SendMessageSuccessArgs arg = new SendMessageSuccessArgs(message.Message);
            SendMessageSuccess.Invoke(_client,arg);
        }

        /// <summary>
        /// client recieves message
        /// </summary>
        public event EventHandler<MessageRecievedArgs> MessageRecieved;
        public void OnMessageRecieved(ClientBroadcastMessage message)
        {
            MessageRecievedArgs args = new MessageRecievedArgs();
            args.ChatId = message.ParentIdChat;
            args.Message = message.Message;
            MessageRecieved.Invoke(_client, args);
        }

        public event EventHandler<ChatRecivedArgs> ChatsRecieved;
        public void OnChatsRecieved(ClientReturnChats message)
        {
            ChatRecivedArgs args = new ChatRecivedArgs();
            args.Chats = message.Chats;
            ChatsRecieved.Invoke(_client,args);
        }


    }
}
