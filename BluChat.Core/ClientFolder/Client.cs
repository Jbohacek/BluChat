using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.Common.DataObjects;
using BluChat.Core.Common.Entities;
using BluChat.Core.Messages;
using BluChat.Core.Messages.SenderReciever;
using SuperSimpleTcp;
using DataReceivedEventArgs = SuperSimpleTcp.DataReceivedEventArgs;

namespace BluChat.Core.ClientFolder
{
    public class Client
    {
        private readonly SimpleTcpClient _client;

        public SimpleTcpClientEvents TcpEvents => _client.Events;

        public SenderUser Sender;
        public bool HasUser => Sender.User != null;

        public bool IsConnected => _client.IsConnected;
        public IpPort Adress { get; set; }
        public MessageClientManager Manager { get; set; }
        public ClientEvents Events { get; set; }

        public Client(string ipAdress, int port)
        {
            Adress = new IpPort(ipAdress, port);
            Sender = new SenderUser() {User = new User(Adress,DateTime.Now)};

            _client = new SimpleTcpClient($"{ipAdress}:{port}");

            Manager = new MessageClientManager(_client, this);

            Events = new ClientEvents(this);

            _client.Events.Connected += Connected;
            _client.Events.Disconnected += Disconnected;
            _client.Events.DataReceived += DataReceived;
        }

        public void Connect()
        {
            if(IsConnected) return;

            _client.Connect();

        }


        public void Disconnect()
        {
            if(!IsConnected) return;
            _client.Disconnect();
        }

        public void SendAuthentication(string username, string password) =>
            Manager.SendAuthentication(username, password);


        static void Connected(object sender, ConnectionEventArgs e)
        {
            Debug.WriteLine($"*** Server {e.IpPort} connected");
        }

        static void Disconnected(object sender, ConnectionEventArgs e)
        {
            Debug.WriteLine($"*** Server {e.IpPort} disconnected");
        }

        void DataReceived(object sender, DataReceivedEventArgs e)
        {
            Manager.RecieveMessage(Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count));
            Debug.WriteLine($"[{e.IpPort}] {Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count)}");
        }

    }
}
