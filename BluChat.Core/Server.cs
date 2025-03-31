using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSimpleTcp;

namespace BluChat.Core
{
    public class Server
    {
        private SimpleTcpServer server { get; set; }

        public Server()
        {
            server = new SimpleTcpServer("127.0.0.1:9000");

            server.Events.ClientConnected += ClientConnected;
            server.Events.ClientDisconnected += ClientDisconnected;
            server.Events.DataReceived += DataReceived;

            // let's go!
            server.Start();

            // once a client has connected...
            server.Send("[ClientIp:Port]", "Hello, world!");

            Console.WriteLine("Server has started in:" + server.Statistics.StartTime.ToShortTimeString());

            while (true)
            {
                string ipPort = Console.ReadLine();

                if(string.IsNullOrEmpty(ipPort)) continue;
                server.Send(ipPort, "test in " + DateTime.Now.ToLongTimeString());
                Console.WriteLine("Message Send");
            }


        }


        static void ClientConnected(object? sender, ConnectionEventArgs e)
        {
            Console.WriteLine($"[{e.IpPort}] client connected");
        }

        static void ClientDisconnected(object? sender, ConnectionEventArgs e)
        {
            Console.WriteLine($"[{e.IpPort}] client disconnected: {e.Reason}");
        }

        static void DataReceived(object? sender, DataReceivedEventArgs e)
        {
            Console.WriteLine($"[{e.IpPort}]: {Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count)}");
        }

    }
}
