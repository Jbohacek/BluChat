using System.Reflection;
using System.Text;
using BluChat.Core;
using BluChat.Core.Logger;
using BluChat.Core.Server;
using BluChat.Core.UserFolder;

namespace BluChat.ServerConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Server.ServerBuilder serverBuild = new Server.ServerBuilder();
            
            serverBuild.SetAdress(new IpPort("127.0.0.1", 9000));
            serverBuild.SetLogger(new Logger());

            Server server = serverBuild.Build();
            server.Start();

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
                        User user = server.Users.Find(x => adress.ToString() == x.adress.ToString());
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
