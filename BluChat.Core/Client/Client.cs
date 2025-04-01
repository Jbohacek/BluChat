using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSimpleTcp;
using DataReceivedEventArgs = SuperSimpleTcp.DataReceivedEventArgs;

namespace BluChat.Core.Client
{
    public class Client
    {
        private readonly SimpleTcpClient _client;

        public SimpleTcpClientEvents Events => _client.Events;

        public Client(string ipAdress, string port = "9000")
        {
            _client = new SimpleTcpClient($"{ipAdress}:{port}");



            _client.Events.Connected += Connected;
            _client.Events.Disconnected += Disconnected;
            _client.Events.DataReceived += DataReceived;

            // let's go!
            _client.Connect();

            // once connected to the server...
            _client.Send("Hello, world!");



            _client.Send("Hello, world!");

        }

        static void Connected(object sender, ConnectionEventArgs e)
        {
            Debug.WriteLine($"*** Server {e.IpPort} connected");
        }

        static void Disconnected(object sender, ConnectionEventArgs e)
        {
            Debug.WriteLine($"*** Server {e.IpPort} disconnected");
        }

        static void DataReceived(object sender, DataReceivedEventArgs e)
        {
            Debug.WriteLine($"[{e.IpPort}] {Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count)}");
        }

    }
}
