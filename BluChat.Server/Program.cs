using System.Reflection;
using System.Text;
using BluChat.Core;
using BluChat.Core.Data;
using BluChat.Core.Logger;
using BluChat.Core.Messages;
using BluChat.Core.Messages.Abstracts;
using BluChat.Core.Messages.Data;
using BluChat.Core.Messages.MessageTypes;
using BluChat.Core.Messages.SenderReciever;
using BluChat.Core.ServerFolder;
using BluChat.Core.UserFolder;
using Microsoft.EntityFrameworkCore;

namespace BluChat.ServerConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Server.ServerBuilder serverBuild = new Server.ServerBuilder();
            
            serverBuild.SetAdress(new IpPort("127.0.0.1", 9000));
            serverBuild.SetLogger(new Logger());
            serverBuild.SetDatabase(new SqlLiteContext("BluChat"));
            serverBuild.SetAdminUserPassword("123456");

            Server server = serverBuild.Build();
            server.Start();

            if (!server.Database.Chats.GetAll().Any(x => x.Name == "TestChat"))
            {
                Chat chat = new Chat("TestChat");
                chat.AddUserToChat(server.Database.Users.GetAdmin());
                server.Database.Chats.Add(chat);
                server.Database.Save();
            }



            MessageSerializer serializer = new MessageSerializer();
            StringMessageBase messageBase = MessageFactory.GetTestMessage();
            messageBase.Sender = new SenderUser(server.Database.Users.GetAdmin());
            messageBase.ParentChat = server.Database.Chats.GetFirstOrDefault(x => x.Name == "TestChat");
            messageBase.SendTime = DateTime.Now;

            string converted = serializer.SerializeMessageToString(messageBase);

            server.MessageManager.RecieveMessage(converted);






            Task.Run(() => { HandleInputs(server); });

            while (true)
            {
                Thread.Sleep(100);
            }
        }

        static void HandleInputs(Server server)
        {
            ServerConsoleInterface consoleInterface = new ServerConsoleInterface(server);

            while (true)
            {
                string? input = Console.ReadLine();
                if(string.IsNullOrEmpty(input)) continue;

                input = input.Trim().ToLower();

                string[] inputs = input.Contains(' ') ? input.Split(' ') : new[] { input };
                

                switch (inputs[0])
                {
                    case "exit":
                        Environment.Exit(0);
                        break;

                    case "list":
                        Console.WriteLine(consoleInterface.GetUsersList());
                        break;

                    case "clear":
                        Console.Clear();
                        break;
                    case "send":
                        if (inputs.Length <= 2) continue;
                        IpPort adress = new IpPort(inputs[1]);
                        User user = server.ConnectedUsers.Find(x => adress.ToString() == x.Adress.ToString());
                        StringBuilder message = new StringBuilder();
                        for (int i = 2; i < inputs.Length; i++)
                        {
                            message.Append(inputs[i]);
                        }

                        server.Send(user, message.ToString());
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(">> Command not found");
                        Console.ResetColor();
                        break;
                }

            }
        }

    }
}
